using System.Windows.Forms;

namespace RSAKeyGen {
	public partial class OutputDialog : Form {
		public OutputDialog(string priv, string pub) {
			InitializeComponent();
			this.pub.Text = pub;
			this.priv.Text = priv;
		}

		private void pub_MouseClick(object sender, MouseEventArgs e) => pub.SelectAll();

		private void priv_MouseClick(object sender, MouseEventArgs e) => priv.SelectAll();
	}
}
