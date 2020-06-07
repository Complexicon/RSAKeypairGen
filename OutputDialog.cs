using System.Windows.Forms;

namespace RSAKeyGen {
	public partial class OutputDialog : Form {
		public OutputDialog(string priv, string pub) {
			InitializeComponent();
			this.pub.Text = pub;
			this.priv.Text = priv;
		}
	}
}
