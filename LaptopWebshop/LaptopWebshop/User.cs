using System;
using System.Security.Cryptography;
using System.Text;

namespace LaptopWebshop
{
	public abstract class User
	{
        public string username { get; protected set; }
        public string name { get; protected set; }
        public string password { get; protected set; }
        public DateOnly birth { get; protected set; }
        public DateTime lastSpin { get; protected set; }
        public string discount { get; protected set; }

        //Alap konstruktor a vendéghez
        public User()
        {
            this.username = string.Empty;
            this.name = string.Empty;
            this.password = string.Empty;
            this.discount = string.Empty;
        }

        //Normál konstruktor
        public User(string username, string name, string password, DateOnly birth)
        {
            this.username = username;
            this.name = name;
            this.password = SHA1(password);
            this.birth = birth;

			//Default értékek beállítása
			this.lastSpin = DateTime.MinValue; //Alapból a legkorábbi dátumot tároljuk, így biztosítva, hogy a még sohasem pörgetett felhasználónál is müködjön majd az ellenőrzés
			this.discount = string.Empty;
        }

        //Fájl beolvasáskor használt konstruktor, ami csak a txt egy sorát kapja meg string-ként és azt feldarabolva nyeri ki az adatokat
        public User(string line)
        {
            string[] t = line.Split(';');

            this.username = t[0];
            this.name = t[1];
            this.password = t[2];

            DateOnly.TryParse(t[3], out DateOnly tempDO);
            this.birth = tempDO;

            DateTime.TryParse(t[4], out DateTime tempDT);
            this.lastSpin = tempDT;

            this.discount = t[5];
        }

        public abstract string Type();

        public bool IsIt(string username, string password) => this.username.Equals(username) && this.password.Equals(SHA1(password));

        //A pörgetéskor csak ezt a metódust kell meghívni paraméterben a nyereménnyel és beállítja az értékeket
        public void Span(string discount="")
        {
            lastSpin = DateTime.Now;
            this.discount = discount;
        }

        //Formázott kiírás a txt-be mentéshez
        public string FormatToTxt() => string.Format("{0};{1};{2};{3};{4};{5}", username, name, password, birth.ToString("yyyy.MM.dd"), lastSpin.ToString("yyyy.MM.dd HH:mm:ss"), discount);

        //ToString()
        public override string ToString() => string.Format("{0}\r\n -Name: {1}\r\n -Password: {2}\r\n -Birthday: {3}\r\n -Type: {4}\r\n -Last spin: {5}\r\n -Discount: {6}", username, name, password, birth.ToString("yyyy.MM.dd"), Type(), lastSpin.ToString("yyyy.MM.dd HH:mm:ss"), discount);

        //Hash password - https://www.codeproject.com/Questions/523323/Encryptingpluspasswordplusinplusc-23
        public static string SHA1(string password) => Convert.ToBase64String(HashAlgorithm.Create("SHA1").ComputeHash(Encoding.Unicode.GetBytes(password)));
    }
}

