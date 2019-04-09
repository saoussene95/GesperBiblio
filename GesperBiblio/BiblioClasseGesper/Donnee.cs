using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace GesperLibrary
{
    public class Donnee
    {
        List<Diplome> lesDiplomes ;
        List<Employe> lesEmployes ;
        List<Service> lesServices ;
        MySqlConnection cnx;
        MySqlCommand cmd=new MySqlCommand();
        MySqlDataReader data;

        public Donnee()
        {
            lesDiplomes = new List<Diplome>();
            lesEmployes = new List<Employe>();
            lesServices = new List<Service>();
            cnx = Connexion.cnx;
            cmd.Connection = cnx;
        }
        public List<string> AfficherServices()
        {
            List<string> list=new List<string>();
            foreach (Service s in lesServices)
            {
                list.Add(s.ToString());
            }
            return list;
        }
         public void ChargerDiplomes()
        {
            cmd.CommandText = "diplome";
            cmd.CommandType = CommandType.TableDirect;
            cnx.Open();
            data = cmd.ExecuteReader();
            while (data.Read())
            {
                lesDiplomes.Add(new Diplome((int)data[0],(string)data[1]));
            }
            cnx.Close();
        }
        public void ChargerServices()
        {
            cmd.CommandText = "service";
            cmd.CommandType = CommandType.TableDirect;
            cnx.Open();
            data = cmd.ExecuteReader();
            while (data.Read())
            {
                switch ((string)data[2])
                {
                    case "P":
                        lesServices.Add(new Service((int)data[0], (string)data[1], (string)data[2], (string)data[3], (int)data[4]));
                        break;
                    case "A":
                        lesServices.Add(new Service((int)data[0], (string)data[1], (string)data[2],(decimal)data[5]));
                        break;
                }
            }
            cnx.Close();
        }
        public void ChargerEmployes()
        {
            cmd.CommandText = "employe";
            cmd.CommandType = CommandType.TableDirect;
            cnx.Open();
            data = cmd.ExecuteReader();
            Service leService=null;
            while (data.Read())
            {
                foreach (Service s in lesServices)
                {
                    if (s.Id == (int)data[6])
                    {
                        leService = s;
                    }
                    
                }
                lesEmployes.Add(new Employe(Convert.ToInt32(data.GetString(0)),data.GetString(1),data.GetString(2),Convert.ToChar(data.GetString(3)),Convert.ToByte(data.GetString(4)),Convert.ToDecimal(data.GetString(5)),leService));
            }
            cnx.Close();
        }
        public void ChargerLesEmployesDesServices()
        {
            MySqlCommand cmd;
            foreach (Service s in lesServices)
            {
                cnx.Open();
                MySqlDataReader data;
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "select * from employe where emp_service = @id";
                cmd.Parameters.Add("@id", MySqlDbType.Int32);
                cmd.Parameters["@id"].Value = s.Id;
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    foreach (Employe e in lesEmployes)
                    {
                        if (e.Id == (int)data[0])
                        {
                            s.AddEmploye(e);
                        }
                    }
                }
                cnx.Close();
            }

        }
        public void ChargerLesEmployesTitulairesDesDiplomes()
        {
            MySqlCommand cmd;
            foreach (Diplome d in lesDiplomes)
            {
                cnx.Open();
                MySqlDataReader data;
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "select * from posseder where pos_employe = @id";
                cmd.Parameters.Add("@id", MySqlDbType.Int32);
                cmd.Parameters["@id"].Value = d.Id;
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    foreach (Employe e in lesEmployes)
                    {
                        if (e.Id == (int)data[1])
                        {
                            d.AddEmploye(e);
                        }
                    }
                }
                cnx.Close();
            }

        }
        public void ChargerLesDiplomeDesEmploye()
        {
            MySqlCommand cmd;
            foreach (Employe e in lesEmployes)
            {
                cnx.Open();
                MySqlDataReader data;
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "select * from posseder where pos_employe = @id";
                cmd.Parameters.Add("@id", MySqlDbType.Int32);
                cmd.Parameters["@id"].Value = e.Id;
                data = cmd.ExecuteReader();
                while (data.Read())
                {
                    foreach (Diplome d in lesDiplomes)
                    {
                        if (d.Id == (int)data[0])
                        {
                            e.AddDiplome(d);
                        } 
                    }
                }
                cnx.Close();
            }
        }
        public void ToutCharger()
        {
            this.ChargerServices();
            this.ChargerDiplomes();
            this.ChargerEmployes();
            this.ChargerLesDiplomeDesEmploye();
            this.ChargerLesEmployesDesServices();
            this.ChargerLesEmployesTitulairesDesDiplomes();
           
        }
        public void Sauvegarder()
        {
            cmd.CommandText = "RemiseAZero";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnx;
            cnx.Open();
            cmd.ExecuteNonQuery();

            //###################################### service svg ##########################################
            cmd.CommandText = "insert into service (ser_id,ser_designation,ser_type,ser_produit,ser_capacite,ser_budget) values(@id,@designation,@type,@produit,@capacite,@budget);";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", MySqlDbType.Int32);
            cmd.Parameters.Add("@designation", MySqlDbType.String);
            cmd.Parameters.Add("@type", MySqlDbType.String);
            cmd.Parameters.Add("@produit", MySqlDbType.String);
            cmd.Parameters.Add("@capacite", MySqlDbType.Int32);
            cmd.Parameters.Add("@budget", MySqlDbType.Decimal);

            foreach (Service s in lesServices)
            {
                cmd.Parameters["@id"].Value = s.Id;
                cmd.Parameters["@designation"].Value = s.Designation;
                cmd.Parameters["@type"].Value = s.Type;
                cmd.Parameters["@produit"].Value = s.Produit;
                cmd.Parameters["@capacite"].Value = s.Capacite;
                cmd.Parameters["@budget"].Value = s.Budget;
                cmd.ExecuteNonQuery();
            }

            //###################################### Diplome svg ##########################################
            cmd.CommandText = "insert into diplome (dip_id,dip_libelle) values(@id, @libelle)";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@libelle", MySqlDbType.String);
            foreach (Diplome d in lesDiplomes)
            {
                cmd.Parameters["@id"].Value = d.Id;
                cmd.Parameters["@libelle"].Value = d.Libelle;
                cmd.ExecuteNonQuery();
            }
            //###################################### employe svg #########################################
            cmd.CommandText = "insert into employe (emp_id,emp_nom,emp_prenom,emp_sexe,emp_cadre,emp_salaire,emp_service) values(@id,@nom,@prenom,@sexe,@cadre,@salaire,@service);";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@nom", MySqlDbType.String);
            cmd.Parameters.Add("@prenom", MySqlDbType.String);
            cmd.Parameters.Add("@sexe", MySqlDbType.VarChar);
            cmd.Parameters.Add("@cadre", MySqlDbType.Byte);
            cmd.Parameters.Add("@salaire", MySqlDbType.Decimal);
            cmd.Parameters.Add("@service", MySqlDbType.Int32);
            foreach (Employe e in lesEmployes)
            {
                cmd.Parameters["@id"].Value = e.Id;
                cmd.Parameters["@nom"].Value = e.Nom;
                cmd.Parameters["@prenom"].Value = e.Prenom;
                cmd.Parameters["@sexe"].Value = e.Sexe;
                cmd.Parameters["@cadre"].Value = e.Cadre;
                cmd.Parameters["@salaire"].Value = e.Salaire;
                cmd.Parameters["@service"].Value = e.LeService.Id;
                cmd.ExecuteNonQuery();
            }
            //#################################### posseder svg ###########################################
            cmd.CommandText = "insert into posseder (pos_diplome,pos_employe) values (@idDiplome,@idEmploye);";
            cmd.CommandType=CommandType.Text;
            cmd.Parameters.Add("@idDiplome", MySqlDbType.Int32);
            cmd.Parameters.Add("@idEmploye", MySqlDbType.Int32);
            foreach (Employe e in lesEmployes)
            {
                foreach (Diplome d in e.LesDiplome)
                {
                    cmd.Parameters["@idDiplome"].Value = d.Id;
                    cmd.Parameters["@idEmploye"].Value = e.Id;
                    cmd.ExecuteNonQuery();
                }
            }
            cnx.Close();
        }
    }
}
