using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Windows.Forms;

namespace Image_RGB
{
	// Token: 0x02000003 RID: 3
	public partial class Form2 : Form
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020DC File Offset: 0x000002DC
		public Form2()
		{
			WebRequest webRequest = WebRequest.Create("https://pastebin.com/raw/u00Fgvc1");
			WebResponse response = webRequest.GetResponse();
			Stream responseStream = response.GetResponseStream();
			string text = string.Empty;
			using (StreamReader streamReader = new StreamReader(responseStream))
			{
				text = streamReader.ReadToEnd();
			}
			bool flag = text.Contains(Form2.GetProcessorID());
			this.InitializeComponent();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002188 File Offset: 0x00000388
		private void button1_Click(object sender, EventArgs e)
		{
			this.dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;...";
			this.dialog.Title = "Select a picture to convert.";
			bool flag = this.dialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				this.pictureBox1.ImageLocation = this.dialog.FileName;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021E4 File Offset: 0x000003E4
		private void button2_Click(object sender, EventArgs e)
		{
			this.textBox1.Text = "";
			this.str = "local imageBytes = {";
			this.pictureBox1.BackColor = Color.Transparent;
			Bitmap bitmap = new Bitmap(this.dialog.FileName);
			int num = int.Parse(this.textBox2.Text);
			for (int i = 0; i < bitmap.Width; i++)
			{
				for (int j = 0; j < bitmap.Height; j++)
				{
					bool flag = i % num == 0 && j % num == 0;
					if (flag)
					{
						Color pixel = bitmap.GetPixel(i, j);
						this.str = string.Concat(new object[]
						{
							this.str,
							"\n {x = ",
							i,
							", y = ",
							j,
							", r = ",
							pixel.R,
							", g = ",
							pixel.G,
							" , b = ",
							pixel.B,
							"}, "
						});
					}
				}
			}
			this.str = string.Concat(new object[]
			{
				this.str,
				"}\r\nlocal X = ",
				this.pictureBox1.Width,
				" local Y = ",
				this.pictureBox1.Height
			});
			this.str += " local P = Instance.new('Part')\r\nP.Name = 'Bitmap'\r\nP.Anchored = true\r\nP.Transparency = 1\r\nP.Position = Vector3.new(0, 0, 0)\r\nP.Parent = game.Workspace\r\n\r\nlocal B = Instance.new('BillboardGui')\r\nB.Name = 'Image'\r\nB.AlwaysOnTop = true\r\nB.Size = UDim2.new(0, X, 0, Y)\r\nB.Parent = P\r\nlocal F = Instance.new('Frame')\r\nF.Name = 'Holder'\r\nF.Size = UDim2.new(1, 0, 1, 0)\r\nF.BackgroundTransparency = 1\r\nF.Parent = B";
			this.str = this.str + "\r\nlocal res = " + this.textBox2.Text;
			this.str += " local current = 1\r\n\r\nlocal cooldown = false\r\n\r\nlocal cooldowncount = 0\r\n\r\nfor _,v in pairs(imageBytes) do\r\n\tlocal frame = Instance.new('Frame')\r\n\tframe.BackgroundColor3 = Color3.new(v.r/255, v.g/255, v.b/255)\r\n\tframe.Size = UDim2.new(0, 1, 0, 1)\r\n\tframe.BorderSizePixel = 0\r\n\tframe.Position = UDim2.new(0, math.floor(v.x/res), 0, math.floor(v.y/res))\r\n\tframe.Parent = F\r\n\tif frame.BackgroundColor3 == Color3.new(0, 0, 0) then\r\n\t\tframe:Destroy()\r\n\tend\r\n\tcurrent = current+1\r\n\tif current % 2 == 0 and not cooldown then\r\n\t\tcooldown = true\r\n\t\twait()\r\n\telseif current % 2 == 0 and cooldown then\r\n\t\tcooldowncount = cooldowncount+1\r\n\t\tif cooldowncount == 25 then\r\n\t\t\tcooldowncount = 0\r\n\t\t\tcooldown = false\r\n\t\tend\r\n\tend\r\nend";
			this.textBox1.Text = this.str;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000206B File Offset: 0x0000026B
		private void Form1_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000206B File Offset: 0x0000026B
		private void pictureBox1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000206B File Offset: 0x0000026B
		private void textBox1_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000206E File Offset: 0x0000026E
		private void button3_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(this.textBox1.SelectedText, true);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000206B File Offset: 0x0000026B
		private void textBox2_TextChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000206B File Offset: 0x0000026B
		private void label1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002083 File Offset: 0x00000283
		private void button3_Click_1(object sender, EventArgs e)
		{
			MessageBox.Show("Saved to decal.lua");
			File.WriteAllText("decal.lua", this.textBox1.Text);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002BC8 File Offset: 0x00000DC8
		public static string GetProcessorID()
		{
			string str = "C";
			ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + str + ":\"");
			managementObject.Get();
			return managementObject["VolumeSerialNumber"].ToString();
		}

		// Token: 0x04000001 RID: 1
		private OpenFileDialog dialog = new OpenFileDialog();

		// Token: 0x04000002 RID: 2
		private string str = "";
	}
}
