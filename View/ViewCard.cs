using System.Text;
using ConsoleApp1.Model;

namespace ConsoleApp1.View
{
    internal class ViewCard
    {
        public void ClearConsole()
        {
            Console.Clear();
        }
        public void DisplayFirstTimeProgram()
        {
            Console.WriteLine("Ce programme consiste à créer et sauvegarder des cartes mentales\n\n");

            Console.WriteLine("Voici les différentes commandes :");
        }
        public string DisplayInstructionsProgram()
        {     
            Console.WriteLine("1: Ajouter une carte\t2: Afficher toutes les cartes\t3: Changer de carte");
            Console.WriteLine("4: Supprimer une carte et ses enfants\t5: Générer un JSON\t6: Vérifier l'emplacement de la carte\t");
            Console.WriteLine("Tapez le numéro de commande que vous souhaitez effectuer : ");

            return Console.ReadLine();
        }

        public string DisplayCreateNewCardInstructions()
        {
            Console.WriteLine("Veuiller donner un nom à la carte que vous voulez créer :");

            return Console.ReadLine();
        }

        public void DisplayCreateNewCard(bool isCardCreated)
        {
            if(isCardCreated)
            {
                Console.Clear();

                Console.WriteLine("Votre carte a bien été créé !\n");
            }
            else
            {
                Console.Clear();

                Console.WriteLine("Le noeud de la carte ne peut pas dépasser le troisième niveau\n");
            }
            
        }

        public string DisplayWhichCardToSelect()
        {
            Console.WriteLine("Sur quelle carte souhaitez-vous vous placer ?\n");

            Console.WriteLine("Si vous ne voulez sélectionner aucune carte, entrez comme valeur \"x\"\n");

            return Console.ReadLine().ToLower();
        }

        public void DisplaySelectedCardSucess(string cardTitle)
        {
            Console.Clear();

            Console.WriteLine("Vous êtes actuellement sur la carte \"{0}\"\n", cardTitle);
        }

        public void DisplaySelectedCardFailed()
        {
            Console.WriteLine("Le nom de la carte que vous avez entré n'est pas valide. Veuillez entrer le nom d'une carte qui existe\n\n");
        }

        public string DisplayDeleteCard()
        {
            Console.WriteLine("Quelle carte souhaitez-vous supprimer ?");

            return Console.ReadLine();
        }

        public void DisplayCardDeleted(string cardTitle)
        {
            Console.WriteLine("La carte \"{0}\" a bien été supprimée\n", cardTitle);
        }

        public string DisplayCardToFind()
        {
            Console.WriteLine("Quelle carte souhaitez-vous trouver ?");

            return Console.ReadLine();
        }

        public void ShowAllCards(List<ModelCard> cards)
        {
            foreach (ModelCard card in cards)
            {
                PrintTree(card);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Génération de la string en format JSON avec toutes les cartes
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public string ShowAllCardsJSON(List<ModelCard> cards)
        {
            StringBuilder json = new StringBuilder();
            json.Append("[");

            for (int i = 0; i < cards.Count; i++)
            {
                // Création de la liste des objets pour chaque carte
                PrintTreeJSON(json, cards[i]);

                if (i != cards.Count - 1)
                {
                    json.Append(',');
                }
            }

            json.Append("]");

            Console.Clear();
            Console.WriteLine(json.ToString());

            Console.WriteLine();

            return json.ToString();
        }

        /// <summary>
        /// Affiche toute l'arborescences de toutes les cartes
        /// </summary>
        /// <param name="card"></param>
        /// <param name="indent"></param>
        /// <param name="isLast"></param>
        public void PrintTree(ModelCard card, string indent = "", bool isLast = true)
        {
            Console.Write(indent);

            // Calculer l'indentation pour afficher la liste des cartes correctement
            if (card.LevelCard != 0)
            {
                if (isLast)
                {
                    Console.Write("└──");
                    indent += "   ";
                }
                else
                {
                    Console.Write("├──");
                    indent += "|   ";
                }
            }

            Console.WriteLine(card.Title);

            // Appel de la même méthode avec les enfants de la carte actuel pour les affichers
            for (int i = 0; i < card.ChildCard.Count; i++)
            {
                PrintTree(card.ChildCard[i], indent, i == card.ChildCard.Count - 1);
            }
        }

        /// <summary>
        /// Affiche le chemin exact d'une carte à chercher
        /// </summary>
        /// <param name="cards"></param>
        public void PrintPathCard(List<ModelCard> cards)
        {
            Console.Clear();

            bool isFirst = true;
            int indent = 0;

            // Calculer l'indentation pour afficher le chemin correctement
            foreach(ModelCard card in cards)
            {
                if(isFirst)
                {
                    isFirst = false;

                    Console.WriteLine(card.Title);
                }
                else
                {
                    for(int i = 0; i < indent; i++)
                    {
                        Console.Write("   ");
                    }

                    Console.Write("└──");

                    Console.WriteLine(card.Title);

                    indent = indent + 1;  
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Création de la string JSON pour chaque carte
        /// </summary>
        /// <param name="json"></param>
        /// <param name="card"></param>
        public void PrintTreeJSON(StringBuilder json, ModelCard card)
        {
            json.Append('{');

            json.Append("\"title\":");
            json.Append('"');
            json.Append(card.Title.Replace("\"", "\\\""));
            json.Append('"');
            json.Append(',');

            json.Append("\"level\":");
            json.Append(card.LevelCard);
            json.Append(',');

            json.Append("\"children\":");
            json.Append('[');

            // Pour chaque enfants de la carte, on initie une nouvelle liste d'objet en appellant la même méthode
            for (int i = 0; i < card.ChildCard.Count; i++)
            {
                PrintTreeJSON(json, card.ChildCard[i]);
                if (i != card.ChildCard.Count - 1)
                {
                    json.Append(',');
                }
            }

            json.Append(']');

            json.Append('}');
        }
    }
}