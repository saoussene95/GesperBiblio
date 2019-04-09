using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
    public class Diplome
    {

        int id;
        string libelle;
        List<Employe> lesEmployes;

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value; }
        }
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public List<Employe> LesEmployes
        {
            get { return lesEmployes; }
            set { lesEmployes = value; }
        }
        public Diplome(int id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
            lesEmployes = new List<Employe>();
        }
        public void AddEmploye(Employe e)
        {
            lesEmployes.Add(e);
        }
        public string ToString()
        {
            return string.Format(id+" "+libelle);
        }
        
        


    }
}
