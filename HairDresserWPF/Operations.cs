using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HairDresserApp2;
using System.Xml.Serialization;

namespace HairDresserWPF
{
    class Operations
    {
        string fileName = "AppointmentsFile.txt";
        string XmlFileName = "allappointments.xml";
        public Operations()
        {
            // Deleting the file if already Exists.
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public BookedAppointments ReadFromBinFile()
        {
            BookedAppointments bookedAppointments = null;
            XmlSerializer serializer = new XmlSerializer(typeof(BookedAppointments));
            StreamReader reader = new StreamReader(XmlFileName);
            bookedAppointments = (BookedAppointments)serializer.Deserialize(reader);
            reader.Close();

            return bookedAppointments;
        }

       
        BookedAppointments bookedAppointments = new BookedAppointments();
        //bookedAppointments.bookedAppointmentsList = new List<Customer>();
        public BookedAppointments WriteToXmlFile(string timeSlot, string name, uint age, decimal height, string creditCard, string gender, string services)
        {
            FileStream fileStream = null;
            Appointment appointment = new Appointment();
            try
            {

                appointment.TimeStamp = timeSlot;
                appointment.AvailedServices = services;
                //Conditions for Client Categorization (Children [Male/Female], Ladies, Gentlemen)
                if (age <= 18)
                {
                    appointment.Customer1 = new Children(name, age, height, creditCard, gender.ToUpper());
                    appointment.Category = Category.Children;
                    ((Children)appointment.Customer1).IsChild = true;

                }
                else
                {
                    if (gender.ToUpper().Equals("F"))
                    {
                        appointment.Customer1 = new Ladies(name, age, height, creditCard, gender.ToUpper());
                        appointment.Category = Category.Ladies;
                        ((Ladies)appointment.Customer1).IsLady = true;
                    }
                    else
                    {
                        appointment.Customer1 = new Gentlemen(name, age, height, creditCard, gender.ToUpper());
                        appointment.Category = Category.Gentlemen;
                        ((Gentlemen)appointment.Customer1).IsGentleman = true;
                    }
                }

                // Adding to the appointment List
                bookedAppointments.Add(appointment);

                XmlSerializer serializer = new XmlSerializer(typeof(BookedAppointments));
                TextWriter t = new StreamWriter(XmlFileName);
                serializer.Serialize(t, bookedAppointments);
                t.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }



            return bookedAppointments;
        }


        // Function to write in Binary File.
        /*   public void WriteIntoBinFile(string timeSlot, string name, uint age, decimal height, string creditCard, string gender, string services)
           {
               FileStream fileStream = null;
               try
               {
                   fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                   BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                   // Writing in file
                   binaryWriter.Write(timeSlot);
                   binaryWriter.Write(name);
                   binaryWriter.Write(age);
                   binaryWriter.Write(height);
                   binaryWriter.Write(creditCard);
                   binaryWriter.Write(gender);
                   binaryWriter.Write(services);
                   binaryWriter.Close();

               }
               catch (IOException e)
               {
                   Console.WriteLine(e.ToString());
               }
               finally
               {
                   if (fileStream != null)
                   {
                       fileStream.Close();
                   }
               }
           }

           // Function to Read from Binary File and construct the Appointment List.
           public BookedAppointments ReadFromBinFile(string fileName)
           {
               string timeSlot = string.Empty;
               string name = string.Empty;
               uint age=0;
               decimal height=0.0m;
               string creditCard= string.Empty;
               string gender=string.Empty;
               string services = string.Empty;
               BookedAppointments bookedAppointments = new BookedAppointments();


               FileStream fileStream = null;
               string row = String.Empty;
               try
               {
                   //Opening the binary file
                   fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                   BinaryReader binaryReader = new BinaryReader(fileStream);


                   // Condition to check End of File
                   while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
                   {
                       Appointment appointment = new Appointment();
                       // Reading from file
                       timeSlot = binaryReader.ReadString();
                       name = binaryReader.ReadString();
                       age = binaryReader.ReadUInt32();
                       height = binaryReader.ReadDecimal();
                       creditCard = binaryReader.ReadString();
                       gender = binaryReader.ReadString();
                       services = binaryReader.ReadString();


                       appointment.TimeStamp = timeSlot;
                       appointment.AvailedServices = services;
                       //Conditions for Client Categorization (Children [Male/Female], Ladies, Gentlemen)
                       if (age <= 18)
                       {
                           appointment.Customer = new Children(name, age, height, creditCard, gender.ToUpper());
                           appointment.Category = Category.Children;
                           ((Children)appointment.Customer).IsChild = true;

                       }
                       else
                       {
                           if (gender.ToUpper().Equals("F"))
                           {
                               appointment.Customer = new Ladies(name, age, height, creditCard, gender.ToUpper());
                               appointment.Category = Category.Ladies;
                               ((Ladies)appointment.Customer).IsLady = true;
                           }
                           else
                           {
                               appointment.Customer = new Gentlemen(name, age, height, creditCard, gender.ToUpper());
                               appointment.Category = Category.Gentlemen;
                               ((Gentlemen)appointment.Customer).IsGentleman = true;
                           }
                       }

                       // Adding to the appointment List
                       bookedAppointments.Add(appointment);
                   }

                   binaryReader.Close();
               }
               catch (IOException e)
               {
                   Console.WriteLine(e.ToString());
               }
               finally
               {
                   if (fileStream != null)
                   {
                       fileStream.Close();
                   }
               }

               // Sorting the Appointment List by Age
               bookedAppointments.Sort();

               return bookedAppointments;
           }*/



    }

}