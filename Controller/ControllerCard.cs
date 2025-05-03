using ConsoleApp1.Model;
using ConsoleApp1.View;

namespace ConsoleApp1.Controller
{
    internal class ControllerCard
    {
        private ViewCard view = new ViewCard();

        public void StartProgram()
        {

            // Affiche l'introduction du programme
            view.DisplayFirstTimeProgram();

            while (true)
            {
                // Affiche toutes les commandes à chaque fin d'exécution d'une d'elles
                switch (view.DisplayInstructionsProgram())
                {
                    // Création d'une carte
                    case "1":
                        view.ClearConsole();

                        bool isCardCreated = ModelCard.CreateNewCard(view.DisplayCreateNewCardInstructions());

                        view.DisplayCreateNewCard(isCardCreated);

                        view.ShowAllCards(ModelCard.GetRootCards());
                        break;

                    // Afficher toutes les cartes et leurs arborescences
                    case "2":
                        view.ClearConsole();

                        view.ShowAllCards(ModelCard.GetRootCards());
                        break;

                    // Changer sur quelle carte on souhaite se situer
                    case "3":
                        view.ClearConsole();

                        view.ShowAllCards(ModelCard.GetRootCards());

                        ModelCard.ChangePointerCard(view);

                        view.ShowAllCards(ModelCard.GetRootCards());
                        break;

                    // Supprimer une cartes et ses enfants
                    case "4":
                        view.ClearConsole();

                        view.ShowAllCards(ModelCard.GetRootCards());

                        // Récupère la liste avec le chemin exact de la carte souhaité
                        string cardTitle = ModelCard.DeleteCard(view.DisplayDeleteCard());

                        view.ClearConsole();

                        // Affiche le chemin de la carte avec une liste calculé
                        view.DisplayCardDeleted(cardTitle);

                        view.ShowAllCards(ModelCard.GetRootCards());
                        break;

                    // Générer le JSON
                    case "5":
                        view.ShowAllCardsJSON(ModelCard.GetRootCards());
                        break;

                    // Trouver une carte et afficher son chemin
                    case "6":
                        view.ClearConsole();

                        view.ShowAllCards(ModelCard.GetRootCards());

                        view.PrintPathCard(ModelCard.FindCard(view.DisplayCardToFind()));
                        break;
                        /*
                    case "7":
                        Card.CheckActiveCard();
                        break;
                    case "8":
                        Card.CheckParentCard();
                        break;
                    case "9":
                        Card.CheckChildrenCard();
                        break;
                    case "10":
                        Card.CheckLevelCard();
                        break;
                    */
                    default:
                        break;
                }
            }
        }
    }
}
