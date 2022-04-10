using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.ViewModels;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ConfectioneryView
{
    public partial class FormCreateOrder : Form
    {
        private readonly IPastryLogic _logicP;
        private readonly IOrderLogic _logicO;
        private readonly IClientLogic _logicС;
        public FormCreateOrder(IPastryLogic logicP, IOrderLogic logicO, IClientLogic logicC)
        {
            InitializeComponent();
            _logicP = logicP;
            _logicO = logicO;
            _logicС = logicC;
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            List<ClientViewModel> clientsList = _logicС.Read(null);
            if (clientsList != null)
            {
                comboBoxClient.DisplayMember = "FIO";
                comboBoxClient.ValueMember = "Id";
                comboBoxClient.DataSource = clientsList;
                comboBoxClient.SelectedItem = null;
            }
            else
            {
                throw new Exception("Не удалось загрузить список клиентов");
            }

            List<PastryViewModel> list = _logicP.Read(null);
            if (list != null)
            {
                comboBoxPastry.DisplayMember = "PastryName";
                comboBoxPastry.ValueMember = "Id";
                comboBoxPastry.DataSource = list;
                comboBoxPastry.SelectedItem = null;
            }
        }
        private void CalcSum()
        {
            if (comboBoxPastry.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxPastry.SelectedValue);
                    PastryViewModel product = _logicP.Read(new PastryBindingModel
                    {
                        Id
                    = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ComboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxPastry.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logicO.CreateOrder(new CreateOrderBindingModel
                {
                    ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    PastryId = Convert.ToInt32(comboBoxPastry.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

