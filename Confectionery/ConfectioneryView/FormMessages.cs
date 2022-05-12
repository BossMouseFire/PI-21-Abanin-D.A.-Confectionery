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
            try
            {
                Program.ConfigGrid(logic.Read(null), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
