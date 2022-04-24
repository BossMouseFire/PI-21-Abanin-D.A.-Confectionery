using System;
using System.Windows.Forms;
using ConfectioneryContracts.BusinessLogicsContracts;

namespace ConfectioneryView
{
    public partial class FormMessages : Form
    {
        private readonly IMessageInfoLogic logic;
        public FormMessages(IMessageInfoLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void FormMessages_Load(object sender, EventArgs e)
        {
            var list = logic.Read(null);
            if (list != null)
            {
                dataGridView.DataSource = list;
                dataGridView.Columns[0].Visible = false;
                dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
