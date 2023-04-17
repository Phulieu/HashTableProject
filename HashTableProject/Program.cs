using HashtableProject;

namespace HashTableProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Linear<Person, Person> linear = new Linear<Person, Person>();
            Quadratic<Person, Person> quadratic = new Quadratic<Person, Person>();
            DoubleHash<Person, Person> doubleHash = new DoubleHash<Person, Person>();


            TestHT(linear);
            TestHT(quadratic);
            TestHT(doubleHash);

        }

        static void TestHT(HashTable<Person,Person> ht)
        {
            LoadDataFromFile(ht);
            Console.WriteLine("HashTable type: " + ht.GetType().ToString());
            Console.WriteLine("# of People ="  +    ht.Count);
            Console.WriteLine("Number of collistion: " + ht.NumCollisions + "\n");
        }

        static void LoadDataFromFile(HashTable<Person,Person> ht)
        {
            StreamReader sr = new StreamReader(File.Open("People.txt", FileMode.Open));
            string sInput = "";
            try
            {
                while ((sInput  = sr.ReadLine()) != null){

                    try
                    {
                        char[] cArray = { ' ' };
                        string[] sArray = sInput.Split(cArray);
                        int iSSN = Int32.Parse(sArray[0]);
                        Person p = new Person(iSSN, sArray[2], sArray[1]);
                        ht.Add(p,p);
                    } catch (ApplicationException ae)
                    {
                        Console.WriteLine(ae.Message);
                    }
                }

            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            sr.Close();

        }
    }
}