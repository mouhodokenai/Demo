using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PartnerCardsWinForms
{
    public partial class Form1 : Form
    {
        private FlowLayoutPanel flowLayoutPanelPartners;

        public Form1()
        {
            InitializeComponent();
            LoadPartners();
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanelPartners = new FlowLayoutPanel();
            this.SuspendLayout();

            this.flowLayoutPanelPartners.Dock = DockStyle.Fill;
            this.flowLayoutPanelPartners.AutoScroll = true;
            this.flowLayoutPanelPartners.FlowDirection = FlowDirection.TopDown;
            this.flowLayoutPanelPartners.WrapContents = false;

            this.Controls.Add(this.flowLayoutPanelPartners);

            this.Text = "Партнёры";
            this.ClientSize = new System.Drawing.Size(350, 400);
            this.ResumeLayout(false);
        }

        private void LoadPartners()
        {
            var partners = GetPartners();
            foreach (var partner in partners)
            {
                PartnerCard card = new PartnerCard();
                card.SetData(partner.Type, partner.Name, partner.Director, partner.Phone, partner.Rating, partner.Discount);
                flowLayoutPanelPartners.Controls.Add(card);
            }
        }

        private List<Partner> GetPartners()
        {
            return new List<Partner>
            {
                new Partner { Type = "Тип", Name = "Наименование партнера", Director = "Директор", Phone = "+7 223 322 22 32", Rating = 10, Discount = 10 },
                new Partner { Type = "Тип", Name = "Наименование партнера", Director = "Директор", Phone = "+7 223 322 22 32", Rating = 10, Discount = 10 },
                new Partner { Type = "Тип", Name = "Наименование партнера", Director = "Директор", Phone = "+7 223 322 22 32", Rating = 10, Discount = 10 },
            };
        }
    }
}