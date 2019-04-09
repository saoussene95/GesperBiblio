using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
    public class Employe
    {
        int id;
        byte cadre;
        decimal salaire;
        string nom, prenom;
        char sexe;
        Service leService;
        List<Diplome> lesDiplomes = new List<Diplome>();


        public Service LeService
        {
            get { return leService; }
        }

        public byte Cadre
        {
            get { return cadre; }
            set { cadre = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public decimal Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }
        public char Sexe
        {
            get { return sexe; }
            set { sexe = value; }
        }
        public List<Diplome> LesDiplome {
            get
            {
                return this.lesDiplomes;
            }
        }
        public void AddDiplome(Diplome d){
            lesDiplomes.Add(d);
        }

        public Employe(int id, string nom, string prenom, char sexe, byte cadre, decimal salaire,Service leService)
        {
            this.cadre = cadre;
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.salaire = salaire;
            this.sexe = sexe;
            this.leService = leService;
        }
        public string ToString()
        {
            return string.Format(id + " " + cadre + " " + salaire + " " + nom + " " + prenom + " " + sexe + " " + leService);
        }
    }
}
