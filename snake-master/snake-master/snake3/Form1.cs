using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake3
{
    public partial class Form1 : Form
    {
        Cola cabeza;
        Comida comida;
        Comida comida2;
        Enemigo obstaculo;
        int puntaje = 0;
        Graphics g;
        int xdir = 0, ydir = 0;
        int cuadro = 10;
        int nivel=1;
        int vel=50;
        int limlevel=2;
        int cambiac = 3;
        int intervalo=100;
       // char color = 'Black';
        Boolean ejex = true, ejey = true;
        public Form1()
        {
            InitializeComponent();
            cabeza = new Cola(10, 10);
            comida = new Comida();
            comida2 = new Comida();
            obstaculo = new Enemigo();
            g = canvas.CreateGraphics();
        }

        public void movimiento()
        {
            cabeza.setxy(cabeza.verX() + xdir, cabeza.verY() + ydir);
        }
        private void bucle_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            cabeza.dibujar(g);
            comida.dibujar(g);
            comida2.dibujar(g);
            //  obstaculo.dibujar(g);
            movimiento();
            choquecuerpo();
            choqueobstaculo();
            choquePared();
             //intervalo = bucle.Interval;

            if (cabeza.interseccion(comida))
            {
                comida.colocar();
               //comida2.colocar();
                cabeza.meter();
                puntaje=puntaje+2;
                if(puntaje==limlevel){
                    obstaculo.colocar();
                    intervalo = intervalo - vel;
                    if (intervalo > 0)
                    {
                        
                        bucle.Interval =intervalo ;
                        limlevel=limlevel+2;
                        nivel++;
                       
                    }


                }
                puntos.Text = puntaje.ToString();
                level.Text = nivel.ToString();
            }
            if (cabeza.interseccion(comida2))
            {
                comida2.colocar();
                // comida2.colocar();
                cabeza.meter();
                puntaje=puntaje+2;
                if (puntaje == limlevel)
                {
                    obstaculo.colocar();
                    intervalo = intervalo - vel;
                    if (intervalo > 0)
                    {

                        bucle.Interval = intervalo;
                        limlevel = limlevel + 2;
                        nivel++;

                    }


                }
                puntos.Text = puntaje.ToString();
                level.Text = nivel.ToString();
            }
        }

        public void choquePared()
        {
            if(cabeza.verX() < 0 || cabeza.verX() > 770 || cabeza.verY() < 0 || cabeza.verY() > 380)
            {
                findejuego();
            }
        }
        public void choqueobstaculo()
        {
            if(obstaculo.verXobs() == cabeza.verX() || obstaculo.verYobs() == cabeza.verY())
            {
               // findejuego();
            }
        }
        public void findejuego()
        {
            puntaje = 0;
            limlevel = 2;
            intervalo = 100;
            nivel = 1;
            bucle.Interval = 100;
            puntos.Text = "0";
            level.Text = "1";
            ejex = true;
            ejey = true;
            xdir = 0;
            ydir = 0;
            cabeza = new Cola(10, 10);
            comida = new Comida();
            MessageBox.Show("Su juego ha terminado");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void choquecuerpo()
        {
            Cola temp;
            try
            {
                temp = cabeza.verSiguiente().verSiguiente();
            } catch (Exception err)
            {
                temp = null;
            }
            while(temp != null)
            {
                if (cabeza.interseccion(temp))
                {
                    findejuego();
                } else {
                    temp = temp.verSiguiente();
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ejex)
            {
                if(e.KeyCode == Keys.Up)
                {
                    ydir = -cuadro;
                    xdir = 0;
                    ejex = false;
                    ejey = true;
                }
                if(e.KeyCode == Keys.Down)
                {
                    ydir = cuadro;
                    xdir = 0;
                    ejex = false;
                    ejey = true;
                }
            }
            if (ejey)
            {
                if(e.KeyCode == Keys.Right)
                {
                    ydir = 0;
                    xdir = cuadro;
                    ejex = true;
                    ejey = false;
                }
                if(e.KeyCode == Keys.Left)
                {
                    ydir = 0;
                    xdir = -cuadro;
                    ejex = true;
                    ejey = false;
                }
            }
        }
    }
}
