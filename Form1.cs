using System;
using System.Drawing;
using System.Windows.Forms;

//när konsollen startar dyker rutan om husdjursval upp
namespace MittHusdjur
{
    public class AnimalSelectionForm : Form
    {
        public string SelectedAnimal { get; private set; }
        private Button catButton;
        private Button dogButton;
        private PictureBox catPictureBox;
        private PictureBox dogPictureBox;

        //välj mellan katt och hund
        public AnimalSelectionForm()
        {
            Text = "Välj ditt husdjur";
            Size = new Size(400, 200);

            //bild och knapp för katt
            catPictureBox = new PictureBox
            {
                Image = Image.FromFile("pic/happycat.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(100, 100),
                Location = new Point(50, 20)
            };
            Controls.Add(catPictureBox);

            catButton = new Button
            {
                Text = "Välj Katt",
                Location = new Point(60, 130)
            };
            catButton.Click += (s, e) => SelectAnimal("Katt");
            Controls.Add(catButton);

            //bild och knapp för hund
            dogPictureBox = new PictureBox
            {
                Image = Image.FromFile("pic/happydog.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(100, 100),
                Location = new Point(220, 20)
            };
            Controls.Add(dogPictureBox);

            dogButton = new Button
            {
                Text = "Välj Hund",
                Location = new Point(230, 130)
            };
            dogButton.Click += (s, e) => SelectAnimal("Hund");
            Controls.Add(dogButton);
        }

        private void SelectAnimal(string animal)
        {
            SelectedAnimal = animal;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    //efteråt måste du namnge ditt husdjur
    public class NameForm : Form
    {
        public string PetName { get; private set; }
        private TextBox nameTextBox;
        private Button okButton;

        public NameForm()
        {
            Text = "Namnge ditt husdjur";
            Size = new Size(300, 150);

            nameTextBox = new TextBox
            {
                Location = new Point(20, 20),
                Width = 240
            };
            Controls.Add(nameTextBox);

            okButton = new Button
            {
                Text = "OK",
                Location = new Point(100, 60)
            };
            okButton.Click += OkButton_Click;
            Controls.Add(okButton);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            //felhantering, det går inte lämna fältet tomt
            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Namnet kan inte vara tomt. Vänligen ange ett namn.");
                return;
            }

            PetName = nameTextBox.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public partial class Form1 : Form
    {
        private Pet myPet;
        private string petType;
        private PictureBox petPictureBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer surpriseEventTimer;
        private Button feedButton, playButton, sleepButton;
        private Label hungerLabel, happinessLabel, energyLabel, nameLabel, timerLabel;
        private int countdown = 30;

        public Form1()
        {
            InitializeComponent();

            //visa djurvalet först
            using (AnimalSelectionForm animalForm = new AnimalSelectionForm())
            {
                if (animalForm.ShowDialog() == DialogResult.OK)
                {
                    petType = animalForm.SelectedAnimal;
                }
                else
                {
                    MessageBox.Show("Inget djur valdes. Applikationen avslutas.");
                    Close();
                    return;
                }
            }

            //visa namnformuläret sedan
            using (NameForm nameForm = new NameForm())
            {
                if (nameForm.ShowDialog() == DialogResult.OK)
                {
                    string petName = string.IsNullOrEmpty(nameForm.PetName) ? "Fluffy" : nameForm.PetName;
                    myPet = new Pet(petName);
                }
                else
                {
                    myPet = new Pet("Fluffy");
                }
            }

            //starta UI-elementen
            InitializeUI();

            //initiera och starta timer1
            timer1 = new System.Windows.Forms.Timer { Interval = 1000 };
            timer1.Tick += timer1_Tick;
            timer1.Start();

            //överraskningstimer
            surpriseEventTimer = new System.Windows.Forms.Timer { Interval = 60000 };
            surpriseEventTimer.Tick += surpriseEventTimer_Tick;
            surpriseEventTimer.Start();
        }


        //klickhantering som gör att användaren kan klappa djuret med muspekaren
        private void PetPictureBox_Click(object sender, EventArgs e)
        {
            //klappa husdjuret
            myPet.Happiness = Math.Min(100, myPet.Happiness + 10); //öka happiness med 10
            UpdateUI(); //uppdatera användargränssnittet
            UpdatePetImage(); //uppdatera bilden
        }

        private void InitializeUI()
        {
            //ställer in knappar
            nameLabel = new Label
            {
                Text = $"Husdjurets namn: {myPet.Name} ({petType})",
                Font = new Font("Arial", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10)
            };
            Controls.Add(nameLabel);

            timerLabel = new Label
            {
                Text = $"Nästa uppdatering om: {countdown} s",
                Font = new Font("Arial", 10, FontStyle.Italic),
                AutoSize = true,
                Location = new Point(10, 400)
            };
            Controls.Add(timerLabel);

            //skapar interaktionsknappar
            feedButton = new Button { Text = "Äta", Location = new Point(10, 50) };
            sleepButton = new Button { Text = "Sova", Location = new Point(10, 130) };
            playButton = new Button { Text = "Leka", Location = new Point(10, 90) };
            Button brushButton = new Button { Text = "Borsta pälsen", Location = new Point(10, 170) };


            //klickhantering
            feedButton.Click += feedButton_Click;
            playButton.Click += playButton_Click;
            sleepButton.Click += sleepButton_Click;
            brushButton.Click += brushButton_Click;



            hungerLabel = new Label { Location = new Point(150, 50), AutoSize = true };
            happinessLabel = new Label { Location = new Point(150, 90), AutoSize = true };
            energyLabel = new Label { Location = new Point(150, 130), AutoSize = true };

            petPictureBox = new PictureBox
            {
                Location = new Point(150, 170),
                Size = new Size(200, 200),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
                petPictureBox.Cursor = Cursors.Hand;
                petPictureBox.Click += PetPictureBox_Click; // Lägg till denna rad för att registrera klickhändelsen
                Controls.Add(petPictureBox);



            Controls.Add(petPictureBox);
            Controls.Add(feedButton);
            Controls.Add(playButton);
            Controls.Add(sleepButton);
            Controls.Add(hungerLabel);
            Controls.Add(happinessLabel);
            Controls.Add(energyLabel);
            Controls.Add(brushButton); 

            UpdateUI();
            UpdatePetImage();
        }

        //en timer som uppdaterar djurets humör automatiskt
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (myPet == null || timerLabel == null)
            {
                return;
            }

            countdown--;
            timerLabel.Text = $"Nästa uppdatering om: {countdown} s";

    if (countdown <= 0)
    {
        //när nedräkningen är klar, uppdatera husdjurets status och återställ nedräkningen
        myPet.UpdateStatus();
        UpdateUI();
        UpdatePetImage();
        countdown = 30; //återställ nedräkningen till 30 sekunder
    }
        UpdateUI();

        }
        private void feedButton_Click(object sender, EventArgs e)
        {
            myPet.Feed();
            UpdateUI();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            myPet.Play();
            UpdateUI();
        }

        private void sleepButton_Click(object sender, EventArgs e)
        {
            myPet.Sleep();
            UpdateUI();
        }
        private void brushButton_Click(object sender, EventArgs e)
        {
            myPet.Happiness = Math.Max(0, myPet.Happiness - 10); //minska happiness med 10
            UpdateUI();
            UpdatePetImage(); //uppdatera bilden för att visa arg husdjursbild om happiness är låg
        }
        private void UpdateUI()
        {
            hungerLabel.Text = $"Hunger: {myPet.Hunger}";
            happinessLabel.Text = $"Lycka: {myPet.Happiness}";
            energyLabel.Text = $"Energi: {myPet.Energy}";


            //färgkodning baserat på status
            hungerLabel.ForeColor = myPet.Hunger < 20 ? Color.Red : Color.Black;
            happinessLabel.ForeColor = myPet.Happiness < 20 ? Color.Red : Color.Black;
            energyLabel.ForeColor = myPet.Energy < 20 ? Color.Red : Color.Black;

    //uppdatera bilden
    UpdatePetImage();
}

//valde att spelet ska avslutas om djuret hungrar ihjäl
private bool isGameOver = false;
private void UpdatePetImage()
{
    try
    {
        //kontrollera om husdjurets hunger är 0 och visa död bild
        if (myPet.Hunger <= 0)
        {
            if (!isGameOver) //kontrollera om spelet redan är över
            {
                isGameOver = true; //sätt värdet till true för att indikera att spelet är över
                petPictureBox.Image = petType == "Katt" 
                    ? Image.FromFile("pic/deadcat.png") //bild för död katt
                    : Image.FromFile("pic/deaddog.png"); //bild för död hund

                //visa meddelande för game over
                MessageBox.Show("Game Over! Du förlorade.", "Spelet är över", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //stäng programmet
                Application.Exit(); // Avsluta applikationen
            }
            return; //avbryt metoden för att förhindra ytterligare uppdateringar
        }


 //kolla om hungern är låg, om ja, visa arg bild
        if (petType == "Katt" && myPet.Hunger < 30)
        {
            petPictureBox.Image = Image.FromFile("pic/angrycat.png"); //bild för arg katt
            return; //avsluta metoden, vi visar inte fler bilder
        }
        else if (petType == "Hund" && myPet.Hunger < 30)
        {
            petPictureBox.Image = Image.FromFile("pic/angrydog.png"); //bild för arg hund
            return; //avsluta metoden, vi visar inte fler bilder
        }


        //snnars, kolla status och visa rätt bild
        if (petType == "Katt")
        {
            if (myPet.Happiness < 30)
            {
                petPictureBox.Image = Image.FromFile("pic/angrycat.png"); //bild för arg katt
            }
            else if (myPet.Energy < 30)
            {
                petPictureBox.Image = Image.FromFile("pic/sleepingcat.png"); //bild för trött katt
            }
            else
            {
                petPictureBox.Image = Image.FromFile("pic/happycat.png"); //bild för glad katt
            }
        }
        else if (petType == "Hund")
        {
            if (myPet.Happiness < 30)
            {
                petPictureBox.Image = Image.FromFile("pic/angrydog.png"); //bild för arg hund
            }
            else if (myPet.Energy < 30)
            {
                petPictureBox.Image = Image.FromFile("pic/sleepingdog.png"); //bild för trött hund
            }
            else
            {
                petPictureBox.Image = Image.FromFile("pic/happydog.png"); //bild för glad hund
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Fel vid inläsning av bild: {ex.Message}");
    }
}
        //en timer som inte syns för användaren, överraskningsmoment
        private void surpriseEventTimer_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int eventChance = rand.Next(1, 101);

            if (eventChance <= 30)
            {
                TriggerSurpriseEvent();
            }
        }

        //kod för att initialisera överraskningsmoment
        private void TriggerSurpriseEvent()
        {
            Random rand = new Random();
            int eventType = rand.Next(1, 5);

            string eventMessage = "";

            switch (eventType)
            {
                case 1:
                    myPet.Happiness += 10;
                    eventMessage = $"{myPet.Name} hittade en ny leksak och blev gladare!";
                    break;
                case 2:
                    myPet.Hunger -= 10;
                    eventMessage = $"{myPet.Name} hittade lite mat och är mindre hungrig.";
                    break;
                case 3:
                    myPet.Energy -= 10;
                    eventMessage = $"{myPet.Name} känner sig lite tröttare.";
                    break;
                case 4:
                    myPet.Happiness -= 15;
                    eventMessage = $"{myPet.Name} blev sjuk och behöver extra omvårdnad.";
                    break;
            }

            UpdateUI();
            UpdatePetImage();

            MessageBox.Show(eventMessage, "Överraskningshändelse");
        }
    }
}