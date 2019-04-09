using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GesperLibrary
{
    public class Service
    {
        List<Employe> lesEmployesDuService = new List<Employe>();
        decimal budget;
        int capacite, dernierId, id;
        string produit, designation;
        string type;

        public string ToString()
        {
            return string.Format(id + " " + designation + " " + type + " " + produit + " " + capacite + " " + budget);
        }
        public Service(int id, string designation, string type, string produit, int capacite)
        {
            this.id = id;
            this.designation = designation;
            this.type = type;
            this.produit = produit;
            this.capacite = capacite;
            this.budget = 0;
        }
        public Service(int id, string designation, string type, decimal budget)
        {
            this.id = id;
            this.designation = designation;
            this.type = type;
            this.produit = "null";
            this.capacite = 0;
            this.budget = budget;
        }

        
        public string Designation
        {
            get { return designation; }
        }

        public string Type
        {
            get { return type; }
        }

        public string Produit
        {
            get { return produit; }
        }

        public int Id
        {
            get { return id; }
        }

        public int DernierId
        {
            get { return dernierId; }
        }

        public int Capacite
        {
            get { return capacite; }
        }


        public decimal Budget
        {
            get { return budget; }
        }

        public void AddEmploye(Employe e){
            lesEmployesDuService.Add(e);
        }
    }
}
