using ConsoleApp1.View;

namespace ConsoleApp1.Model
{
    internal class ModelCard
    {
        private static ModelCard activeCard;

        private static List<ModelCard> cards = new List<ModelCard>();

        // Création du consctructeur pour générer des cartes qui possederont un titre, une carte parent, possiblement des cartes enfants
        // et le niveau de la carte
        public string Title { get; set; }
        public ModelCard ParentCard { get; set; }
        public List<ModelCard> ChildCard { get; set; } = new List<ModelCard>();
        public int LevelCard { get; set; }

        public ModelCard(string title, int levelCard)
        {
            this.Title = title;
            this.LevelCard = levelCard;
        }

        public ModelCard(string title, int levelCard, ModelCard parentCard) :this(title, levelCard)
        {
            this.ParentCard = parentCard;
        }

        /// <summary>
        /// Retourne toutes les cartes
        /// </summary>
        /// <returns></returns>
        public static List<ModelCard> GetCards()
        {
            return cards;
        }

        /// <summary>
        /// Retourne toutes les cartes de niveau 0
        /// </summary>
        /// <returns></returns>
        public static List<ModelCard> GetRootCards()
        {
            List<ModelCard> rootCards = new List<ModelCard>();

            foreach (ModelCard card in cards)
            {
                if (card.LevelCard == 0)
                {
                    rootCards.Add(card);
                }
            }

            return rootCards;
        }

        /// <summary>
        /// Création d'une nouvelle carte si le niveau ne dépasse pas 2
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public static bool CreateNewCard(string inputUser)
        {
            ModelCard card;

            // Si la carte créé n'est pas une carte de niveau 0
            if (activeCard != null)
            {
                if (activeCard.LevelCard >= 2)
                {
                    return false;
                }

                ModelCard parentCard = activeCard;

                // Création de la carte avec son titre, son niveau et son parent
                card = new ModelCard(inputUser, parentCard.LevelCard + 1, parentCard);

                activeCard = card;

                parentCard.ChildCard.Add(activeCard);
            }
            else
            {
                // Création de la carte avec son titre et le niveau défini à 0
                card = new ModelCard(inputUser, 0);

                activeCard = card;
            }

            // Ajouter la carte à la liste de toutes les cartes
            cards.Add(card);

            return true;
        }

        /// <summary>
        /// Changer de carte active
        /// </summary>
        /// <param name="view"></param>
        public static void ChangePointerCard(ViewCard view)
        {
            bool checkTitleCard = false;
            string inputUser;

            // Tant que la valeur rentré n'est pas le nom d'une carte
            do
            {
                inputUser = view.DisplayWhichCardToSelect();

                foreach (ModelCard card in cards)
                {
                    if (inputUser.ToLower() == card.Title.ToLower())
                    {
                        activeCard = card;

                        checkTitleCard = true;

                        view.DisplaySelectedCardSucess(activeCard.Title);

                        break;
                    }
                }

                // Si l'utilisateur ne souhaite pas entrer le nom d'une carte et retourner au niveau 0
                if (inputUser == "x")
                {
                    activeCard = null;
                    break;
                }

                if(checkTitleCard == false)
                {
                    view.DisplaySelectedCardFailed();
                }

            } while (checkTitleCard == false);
        }

        /// <summary>
        /// Vérifier quelle est la carte active
        /// </summary>
        public static void CheckActiveCard()
        {
            if (activeCard != null)
            {
                Console.WriteLine(activeCard.Title);
            }
            else
            {
                Console.WriteLine("Aucune carte n'est sélectionnée");
            }
        }

        /// <summary>
        /// Vérifier la carte parent de la carte active
        /// </summary>
        public static void CheckParentCard()
        {
            if (activeCard.ParentCard != null)
            {
                Console.WriteLine(activeCard.ParentCard.Title);
            }
            else
            {
                Console.WriteLine("La carte ne possède aucun parent");
            }

        }

        /// <summary>
        /// Vérifier le niveau de la carte active
        /// </summary>
        public static void CheckLevelCard()
        {
            if (activeCard != null)
            {
                Console.WriteLine(Convert.ToString(activeCard.LevelCard));
            }
            else
            {
                Console.WriteLine("Aucune carte n'est sélectionnée");
            }
        }

        /// <summary>
        /// Vérifier la liste des enfants de la carte active
        /// </summary>
        public static void CheckChildrenCard()
        {
            foreach (ModelCard card in activeCard.ChildCard)
            {
                Console.WriteLine(card.Title);
            }
        }

        /// <summary>
        /// Supression d'une carte ainsi que de tous ses enfants
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        public static string DeleteCard(string inputUser)
        {
            List<ModelCard> cardsToDelete = new List<ModelCard>();

            foreach (ModelCard card in cards.ToList())
            {
                if (card.Title.ToLower() == inputUser.ToLower())
                {
                    // Défini la carte du niveau en dessous comme nouvelle carte active
                    if(card.ParentCard != null)
                    {
                        activeCard = card.ParentCard;
                    }
                    else
                    {
                        activeCard = null;
                    }

                    // Ajoute les carte enfants à la liste à supprimer
                    SelectCardToDelete(cardsToDelete, card);

                    // Ajoute la carte entrée par l'utilisateur à la liste à supprimer
                    cardsToDelete.Add(card);
                }
            }

            ModelCard cardParent;

            // Supprimer la référence sur les cartes parents pour le paramètre des enfants
            foreach (ModelCard card in cardsToDelete)
            {
                // Supprimer la référence de l'enfant pour la carte parent ciblé
                if (card.ParentCard != null)
                {
                    cardParent = card.ParentCard;

                    cardParent.ChildCard.Remove(card);
                }


                // Supprime tous les enfants de la carte initial
                if (card.ChildCard.Count > 0)
                {
                    int i = 0;

                    do
                    {
                        card.ChildCard.Remove(card.ChildCard[i]);

                    } while (card.ChildCard.Count != 0);
                }

                cards.Remove(card);
            }

            return inputUser;
        }

        /// <summary>
        /// Ajouter toutes les cartes enfants et sous enfants à la liste à supprimer
        /// </summary>
        /// <param name="cardsToDelete"></param>
        /// <param name="card"></param>
        public static void SelectCardToDelete(List<ModelCard> cardsToDelete, ModelCard card)
        {
            for (int i = 0; i < card.ChildCard.Count; i++)
            {
                cardsToDelete.Add(card.ChildCard[i]);

                SelectCardToDelete(cardsToDelete, card.ChildCard[i]);
            }
        }

        /// <summary>
        /// Trouver une carte et afficher son chemin exact
        /// </summary>
        /// <param name="cardTitle"></param>
        /// <returns></returns>
        public static List<ModelCard> FindCard(string cardTitle)
        {
            List<ModelCard> listCard = new List<ModelCard>();

            foreach(ModelCard card in cards)
            {
                if(card.Title.ToLower() == cardTitle.ToLower())
                {
                    listCard.Add(card);

                    FindCardCorrectPath(listCard, card);
                }
            }

            listCard.Reverse();

            return listCard;
        }

        /// <summary>
        /// Trouver les parents de la carte jusqu'à une carte de niveau 0. Ajoute le tout dans la liste de carte
        /// </summary>
        /// <param name="listCards"></param>
        /// <param name="card"></param>
        public static void FindCardCorrectPath(List<ModelCard> listCards, ModelCard card)
        {
            if(card.ParentCard != null)
            {
                listCards.Add(card.ParentCard);

                FindCardCorrectPath(listCards, card.ParentCard);
            }
        }
    }
}