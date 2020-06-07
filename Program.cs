using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace RSAKeyGen {
	class Program {

		[STAThread]
		static void Main(string[] args) {

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			RSACryptoServiceProvider r;

			string pub;
			string priv;

			var strong = MessageBox.Show("Create Strong Key? (4096-Bit) Otherwise (2048-Bit)", "Key Strength", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			var fancy = MessageBox.Show("Fancy Output XML?", "Fanciness", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

			long start = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

			if(strong == DialogResult.Yes) r = new RSACryptoServiceProvider(4096);
			else r = new RSACryptoServiceProvider(2048);


			if(fancy == DialogResult.Yes) {
				pub = FormatXml(r.ToXmlString(false));
				priv = FormatXml(r.ToXmlString(true));
			} else {
				pub = r.ToXmlString(false);
				priv = r.ToXmlString(true);
			}

			double duration = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - start) / 1000.0;

			MessageBox.Show("Done! Took " + duration + "s", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

			var save = MessageBox.Show("Save To File?", "Wanna Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if(save == DialogResult.Yes) {
				FolderBrowserDialog dialog = new FolderBrowserDialog();
				var dialogRes = dialog.ShowDialog();
				if(dialogRes == DialogResult.OK) {
					File.WriteAllText(Path.Combine(dialog.SelectedPath, "private.xml"), priv);
					File.WriteAllText(Path.Combine(dialog.SelectedPath, "public.xml"), pub);
				}
			}

			new OutputDialog(priv, pub).ShowDialog();

		}

		public static string FormatXml(string inputXml) {
			XmlDocument document = new XmlDocument();
			document.Load(new StringReader(inputXml));

			StringBuilder builder = new StringBuilder();
			using(XmlTextWriter writer = new XmlTextWriter(new StringWriter(builder))) {
				writer.Formatting = Formatting.Indented;
				document.Save(writer);
			}

			return builder.ToString();
		}

	}
}
