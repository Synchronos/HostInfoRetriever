using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Management;
using System.Reflection;
using System.Text;
using System.Runtime.Remoting;

namespace Host_Info_Retriever
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class HostInfoRetriever : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblHost;
		private System.Windows.Forms.TextBox txtHost;
		private System.Windows.Forms.TextBox txtHostResults;
		private System.Windows.Forms.Button btnGetInfo;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.CheckBox ckbGetInstalledSoftware;
		private System.Windows.Forms.CheckBox ckbGetHardware;
		private System.Windows.Forms.Label lblHostResults;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.CheckBox ckbGetOtherInfo;

		public HostInfoRetriever()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new HostInfoRetriever());
		}

		/// <summary>
		/// This is the button click method that does all the work.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGetInfo_Click(object sender, System.EventArgs e)
		{
			this.txtHostResults.AppendText(String.Format("Querying {0}...\r\n", this.txtHost.Text));
			this.txtHostResults.Refresh();

			// Query DNS for the address info
			if (this.txtHost.Text != ".")
			{
				this.txtHostResults.AppendText("\tDNS Results:\r\n");
                Ping pinger = new Ping();

				try
				{
					IPHostEntry host = Dns.GetHostEntry(this.txtHost.Text);
					this.txtHostResults.AppendText("\t\tHostname: " + host.HostName + "\r\n");
					for (int i = 0; i < host.AddressList.Length; i++)
					{
						this.txtHostResults.AppendText(String.Format("\t\tIP Address: {0}", host.AddressList[i].ToString()));
						
						this.txtHostResults.AppendText(" (");
						for (int j = 0; j < 3; j++)
						{
                            PingReply reply = pinger.Send(host.AddressList[i].ToString());

							if (reply.Status == IPStatus.Success)
							{
								long ms = reply.RoundtripTime;

                                this.txtHostResults.AppendText(String.Format("{0} ms ", ms));
                            }
                            else
                            {
                                this.txtHostResults.AppendText("* ");
                            }
						}

						this.txtHostResults.Text = this.txtHostResults.Text.Remove(this.txtHostResults.TextLength - 1, 1);
						this.txtHostResults.AppendText(")\r\n");
					}
				}
				catch (System.Net.Sockets.SocketException ex)
				{
					this.txtHostResults.AppendText("\t\tError: " + ex.Message);
				}

				this.txtHostResults.AppendText("\r\n");
			}

			//Declare the Management Scope and other variables.
			ManagementScope scope = null;
			WqlObjectQuery objectQuery = null;
			ManagementObjectSearcher searcher = null;

			if (this.ckbGetOtherInfo.Checked || this.ckbGetHardware.Checked || this.ckbGetInstalledSoftware.Checked)
			{
				// Build an options object for the connection
				ConnectionOptions options = new ConnectionOptions();

				// Use a username and password if supplied, otherwise impersonate the current user.
				if (this.txtUsername.TextLength > 0)
				{
					options.Username = txtUsername.Text;
					options.Password = txtPassword.Text;
				}
				else
				{
					options.Impersonation = ImpersonationLevel.Impersonate;
				}

				// Set up a connection to a remote computer using these options
				scope = new ManagementScope(@"\\" + this.txtHost.Text + @"\root\cimv2", options);
				this.txtHostResults.Text += "\tAttempting WMI connection...\r\n";

				// Make the connection and catch and report errors without blowing up.
				try
				{
					scope.Connect();
				}
				catch (System.Runtime.InteropServices.COMException ex)
				{
					this.txtHostResults.Text += "\t\tError: " + ex.Message + "\r\nDone.\r\n\r\n";
					return;
				}
				catch (System.UnauthorizedAccessException ex)
				{
					this.txtHostResults.Text += "\t\tError: " + ex.Message + "\r\nDone.\r\n\r\n";
					return;
				}

				if (scope.IsConnected)
				{
					this.txtHostResults.Text += "\t\tConnected.\r\n\r\n";
				}

				objectQuery = new WqlObjectQuery();
			}

			if (this.ckbGetOtherInfo.Checked && scope.IsConnected)
			{
				// Set up our query.
				objectQuery = new WqlObjectQuery();
				objectQuery.QueryString = "select * from Win32_ComputerSystem";

				// Perform the search.
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

				// Iterate through the collection of objects returned.
				foreach (ManagementObject computer in searcher.Get()) 
				{
					// Display the items that are of interest to us.
					this.txtHostResults.Text += "\tWindows Info:\r\n";
					this.txtHostResults.Text += "\t\tDomain Role = " + System.Enum.GetName(typeof(enumDomainRole), computer["DomainRole"]).Replace("_", " ") + "\r\n";
					this.txtHostResults.Text += "\t\tDomain = " + computer["Domain"] + "\r\n";
					this.txtHostResults.Text += "\t\tName = " + computer["Name"] + "\r\n";
					this.txtHostResults.Text += "\t\tUser = " + computer["UserName"] + "\r\n";
					this.txtHostResults.Text += "\r\n";
					this.txtHostResults.Text += "\tComputer:\r\n";
					this.txtHostResults.Text += "\t\tManufacturer = " + computer["Manufacturer"] + "\r\n";
					this.txtHostResults.Text += "\t\tModel = " + computer["Model"] + "\r\n";
					this.txtHostResults.Text += "\t\tTotal Physical Memory = " + ((System.UInt64) computer["TotalPhysicalMemory"]) / 1024 / 1024 + " MB\r\n";
					this.txtHostResults.Text += "\r\n";

					computer.Dispose();
				}

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();

				// Change the query to get volume info.
				objectQuery.QueryString = "select * from Win32_LogicalDisk";
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

				this.txtHostResults.Text += "\tDisk Volumes:\r\n";
				// Iterate through the collection of objects returned.
				foreach (ManagementObject volume in searcher.Get()) 
				{
					// Show only the disk drive types that are local to the system.
					switch ((System.UInt32) volume["DriveType"])
					{
						case 2:
						case 3:
						case 5:
							// Display the items that are of interest to us.
							this.txtHostResults.Text += "\t\tDrive Letter = " + volume["DeviceID"] + "\r\n";
							this.txtHostResults.Text += "\t\tDescription = " + volume["Description"] + "\r\n";
							if (volume["FileSystem"] != null)
							{
								this.txtHostResults.Text += "\t\tFile System = " + volume["FileSystem"] + "\r\n";
							}
							this.txtHostResults.Text += "\t\tVolume Name = " + volume["VolumeName"] + "\r\n";
							this.txtHostResults.Text += "\t\tVolume Serial Number = " + volume["VolumeSerialNumber"] + "\r\n";
//							if (volume["VolumeDirty"] != null)
//							{
//								this.txtHostResults.Text += "\t\tVolume Dirty = " + ((bool) volume["VolumeDirty"]).ToString() + "\r\n";
//							}
							if (volume["Size"] != null)
							{
								this.txtHostResults.Text += "\t\tCapacity = " + ((System.UInt64) volume["Size"] / 1024 / 1024) + " MB\r\n";
								this.txtHostResults.Text += "\t\tFree Space = " + ((System.UInt64) volume["FreeSpace"] / 1024 / 1024) + " MB\r\n";
							}
							this.txtHostResults.Text += "\r\n";
							volume.Dispose();

							break;
					}
				}

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();

                // Set up our query.
				objectQuery = new WqlObjectQuery();
				objectQuery.QueryString = "select * from Win32_ScheduledJob";

				// Perform the search.
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

                this.txtHostResults.AppendText("\tAT Scheduled Tasks: \r\n");
				// Iterate through the collection of objects returned.
				foreach (ManagementObject job in searcher.Get()) 
                {
                    dumpObjectFields(job, 2);
                    job.Dispose();
                }

                this.txtHostResults.AppendText("\r\n");

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();
			}

			if (this.ckbGetHardware.Checked && scope.IsConnected)
			{
				// Change the query to get BIOS info.
				objectQuery.QueryString = "select * from Win32_BIOS";
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

				this.txtHostResults.Text += "\tBIOS:\r\n";
				// Iterate through the collection of objects returned.
				foreach (ManagementObject bios in searcher.Get()) 
				{
					// Display the items that are of interest to us.
					this.txtHostResults.Text += "\t\tVersion = " + bios["Name"] + "\r\n";
					this.txtHostResults.Text += "\t\tSerial Number = " + bios["SerialNumber"] + "\r\n";
					this.txtHostResults.Text += "\r\n";

					bios.Dispose();
				}

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();

				this.txtHostResults.Text += "\tProcessor:\r\n";

				// Change the query to get CPU info.
				objectQuery.QueryString = "select * from Win32_Processor";
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

				// Iterate through the collection of objects returned.
				foreach (ManagementObject processor in searcher.Get()) 
				{
					// Display the items that are of interest to us.
					this.txtHostResults.Text += "\t\tName = " + processor["Name"] + "\r\n";
					this.txtHostResults.Text += "\t\tCaption = " + processor["Caption"] + "\r\n";
					this.txtHostResults.Text += "\t\tFSB Clock = " + processor["ExtClock"] + " MHz \r\n";
					this.txtHostResults.Text += "\t\tMaximum Clock Speed = " + processor["MaxClockSpeed"] + " MHz \r\n";
					this.txtHostResults.Text += "\t\tCurrent Clock Speed = " + processor["CurrentClockSpeed"] + " MHz \r\n";
					if (processor["CurrentVoltage"] != null)
					{
						this.txtHostResults.Text += "\t\tCurrent Voltage = " + ((System.UInt16) processor["CurrentVoltage"] / 10.0) + " V \r\n";
					}
					else
					{
						this.txtHostResults.Text += "\t\tCurrent Voltage = Not Available\r\n";
					}
					this.txtHostResults.Text += "\t\tProcessor ID = " + processor["ProcessorID"] + "\r\n";
					this.txtHostResults.Text += "\r\n";

					processor.Dispose();
				}

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();

				// Change the query to get memory info.
				objectQuery.QueryString = "select * from Win32_PhysicalMemory";
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

				this.txtHostResults.Text += "\tMemory:\r\n";
				// Iterate through the collection of objects returned.
				foreach (ManagementObject memory in searcher.Get()) 
				{
					// Display the items that are of interest to us.
					this.txtHostResults.AppendText(String.Format("\t\tLocation = {0}\r\n", memory["DeviceLocator"]));
					this.txtHostResults.Text += "\t\tCapacity = " + ((System.UInt64) memory["Capacity"] / 1024 / 1024) + " MB\r\n";
					this.txtHostResults.Text += "\t\tType = " + System.Enum.GetName(typeof(enumMemoryType), memory["MemoryType"]).Replace("_", " ") + "\r\n";
					this.txtHostResults.Text += "\t\tSpeed = " + memory["Speed"] + " MHz \r\n";
					this.txtHostResults.Text += "\r\n";

					memory.Dispose();
				}

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();
                
				// Change the query to get disk info.
				objectQuery.QueryString = "select * from Win32_DiskDrive";
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

				this.txtHostResults.Text += "\tDisk Drives:\r\n";
				// Iterate through the collection of objects returned.
				foreach (ManagementObject disk in searcher.Get()) 
				{
					// Display the items that are of interest to us.
					this.txtHostResults.Text += "\t\tDevice ID = " + disk["DeviceID"] + "\r\n";
					this.txtHostResults.Text += "\t\tModel = " + disk["Model"] + "\r\n";
					if (disk["Status"] != null)
					{
						this.txtHostResults.Text += "\t\tSMART Status = " + disk["Status"] + "\r\n";
					}
					this.txtHostResults.Text += "\t\tCapacity = " + ((System.UInt64) disk["Size"] / 1024 / 1024) + " MB\r\n";
					this.txtHostResults.Text += "\r\n";

					disk.Dispose();
				}

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();
			}

			if (this.ckbGetInstalledSoftware.Checked && scope.IsConnected)
			{
				// Change the query to get OS info.
				objectQuery.QueryString = "select * from Win32_OperatingSystem";
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

				this.txtHostResults.Text += "\tOperating System:\r\n";

				// Iterate through the collection of objects returned.
				foreach (ManagementObject os in searcher.Get()) 
				{
					// Display the items that are of interest to us.
					this.txtHostResults.Text += "\t\tOperating System = " + os["Caption"] + "\r\n";
					this.txtHostResults.Text += "\t\tServicePack = SP" + os["ServicePackMajorVersion"] + "\r\n";
					this.txtHostResults.Text += "\t\tTotal Visible Memory = " + ((System.UInt64) os["TotalVisibleMemorySize"]) / 1024 + " MB\r\n";
					this.txtHostResults.Text += "\r\n";

					os.Dispose();
				}

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();

				// Change the query to get info about installed software.
				objectQuery.QueryString = "select * from Win32_Product";
				searcher =
					new ManagementObjectSearcher(scope, objectQuery);

				this.txtHostResults.Text += "\tInstalled Software:\r\n";

				// Iterate through the collection of objects returned.
				foreach (ManagementObject product in searcher.Get()) 
				{
					// Display the items that are of interest to us.
					this.txtHostResults.Text += "\t\tName = " + product["Name"] + "\r\n";
					this.txtHostResults.Text += "\t\tVersion = " + product["Version"] + "\r\n";
					this.txtHostResults.Text += "\t\tDescription = " + product["Description"] + "\r\n";
					this.txtHostResults.Text += "\t\tVendor = " + product["Vendor"] + "\r\n";
					if (product["InstallDate"] != null)
					{
						this.txtHostResults.Text += "\t\tInstall Date = " + DateTime.ParseExact((string) product["InstallDate"], "yyyymmdd", new System.Globalization.CultureInfo("en-US", true)).ToLongDateString() + "\r\n";
					}
					else
					{
						this.txtHostResults.Text += "\t\tInstall Date = Not Available\r\n";
					}
					this.txtHostResults.Text += "\t\tInstall State = " + System.Enum.GetName(typeof(enumInstallState), product["InstallState"]).Replace("_", " ") + "\r\n";
					this.txtHostResults.Text += "\r\n";

					product.Dispose();
				}

				this.txtHostResults.Text += "\r\n";

				// Scroll to the end of the text box each time we do this.
				this.txtHostResults.Focus();
				this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
				this.txtHostResults.SelectionLength = 0;
				this.txtHostResults.ScrollToCaret();
			}

			this.txtHostResults.Text += "Done.\r\n\r\n";
			
			// Scroll to the end of the text box each time we do this.
			this.txtHostResults.Focus();
			this.txtHostResults.SelectionStart = this.txtHostResults.TextLength;
			this.txtHostResults.SelectionLength = 0;
			this.txtHostResults.ScrollToCaret();

			searcher.Dispose();
		}

		private void btnCopy_Click(object sender, System.EventArgs e)
		{
			this.txtHostResults.SelectAll();
			this.txtHostResults.Copy();
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.txtHostResults.Clear();
		}

        private string dumpObjectFields(object dumpObject, int tabLevel)
        {
            StringBuilder dump = new StringBuilder();
            FieldInfo[] infos;
            infos = dumpObject.GetType().GetFields();
            foreach (FieldInfo info in infos)
            {
                for (int tabCount = 0; tabCount < tabLevel; tabCount++)
                {
                    dump.Append("\t");
                }

                dump.AppendFormat("{0}: {1}<br>\r\n", info.Name, info.GetValue(dumpObject));
            }

            return dump.ToString();
        }
	}
}
