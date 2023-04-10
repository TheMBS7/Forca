using System.Data;

namespace Forca
{
    public partial class Forca : Form
    {
        private int ContErros;
        private char[] VetorPalavra = default!;
        private Label[] Labels;
        private Label[] Corpo;

        public Forca()
        {
            InitializeComponent();

            ContErros = 0;
            Corpo = new Label[6] { lblCabeca, lblCorpo, lblBracoE, lblBracoD, lblPernaE, lblPernaD };
            Labels = new Label[12] { lbl0, lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10, lbl11 };
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            MessageBox.Show("Escolha um tema para começar!");
        }

        public void EscolheTema(string arquivoBanco)
        {
            Reset();
            string[] banco = File.ReadAllLines(arquivoBanco);
            Random palavraAleatoria = new Random();
            int posicao = palavraAleatoria.Next(banco.Length);
            string palavra = banco[posicao];
            VetorPalavra = palavra.ToCharArray();
            Tamanho();
        }

        public void Reset()
        {
            ContErros = 0;
            txtErro.Text = "";
            for (int X = 0; X < Corpo.Length; X++)
            {
                Corpo[X].Visible = false;
            }
            for (int X = 0; X < Labels.Length; X++)
            {
                Labels[X].Text = "___";
                Labels[X].Visible = false;
            }
        }

        public void Tamanho()
        {
            lblDica.Text = $"A palavra tem {VetorPalavra.Length} letras.";

            for (int i = 0; i < VetorPalavra.Length; i++)
            {
                Labels[i].Visible = true;
            }
        }

        public void Verificacao()
        {
            bool Achou = false;
            for (int i = 0; i < VetorPalavra.Length; i++)
            {
                if (Convert.ToChar(txtTentativa.Text.ToUpper()) == VetorPalavra[i])
                {
                    Achou = true;
                    Labels[i].Text = txtTentativa.Text.ToUpper();
                }
            }
            if (Achou == false)
            {
                if (txtErro.Text.Contains(txtTentativa.Text))
                {
                    MessageBox.Show("Essa letra já foi!");
                }
                else
                {
                    Corpo[ContErros].Visible = true;
                    ContErros++;
                    string palavraAnterior = txtTentativa.Text + "-";
                    txtErro.Text += palavraAnterior.ToUpper();
                }
            }
        }

        public void Vitoria()
        {
            bool Ganhou = true;
            for (int x = 0; x < VetorPalavra.Length; x++)
            {
                if (Labels[x].Text == "___")
                {
                    Ganhou = false;
                    break;
                }
            }
            if (Ganhou == true)
            {
                DialogResult ResultadoDialogo = MessageBox.Show("XAMBRA FIO");
                if (ResultadoDialogo == DialogResult.OK)
                {
                    Reset();
                }
            }
            if (ContErros == 6)
            {
                if (MessageBox.Show("F IN THE CHAT") == DialogResult.OK)
                {
                    Reset();
                }
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Verificacao();
            Vitoria();
        }

        private void btnAnimal_Click(object sender, EventArgs e)
        {
            EscolheTema("BancoDeDados.txt");
        }

        private void btnObjeto_Click(object sender, EventArgs e)
        {
            EscolheTema("BancoDeDadosObj.txt");
        }

        private void btnCarro_Click(object sender, EventArgs e)
        {
            EscolheTema("BancoDeDadosCarro.txt");
        }
    }
}