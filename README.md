# MindMap CLI

## Installation

Pour installer le programme, il suffit de télécharger le dépôt et de lancer l'application nommée ``mindmap-cli``. Il s'agit d'un fichier Raccourci qui pointe sur le fichier situé dans ``bin -> x64 -> Release -> net8.0 -> mindmap-cli``.

## Utilisation

Lors du lancement du programme, vous aller avoir une console avec un affichage comme celui-ci:
```
Ce programme consiste à créer et sauvegarder des cartes mentales

Voici les différentes commandes :
1: Ajouter une carte    2: Afficher toutes les cartes   3: Changer de carte
4: Supprimer une carte et ses enfants   5: Générer un JSON      6: Vérifier l'emplacement de la carte
Tapez le numéro de commande que vous souhaitez effectuer :
```

Pour créer une carte, entrée comme valeur ``1``.

```
Veuiller donner un nom à la carte que vous voulez créer :
```

Donnez un nom à votre carte. Comme par exemple ``Maison``:

```
Votre carte a bien été créé !

Maison

1: Ajouter une carte    2: Afficher toutes les cartes   3: Changer de carte
4: Supprimer une carte et ses enfants   5: Générer un JSON      6: Vérifier l'emplacement de la carte
Tapez le numéro de commande que vous souhaitez effectuer :
```

La carte active se situe maintenant sur ``Maison``. Si on décide de créer une nouvelle carte à nouveau, la nouvelle carte sera en lien avec ``Maison``.

```
Votre carte a bien été créé !

Maison
└──Salon

1: Ajouter une carte    2: Afficher toutes les cartes   3: Changer de carte
4: Supprimer une carte et ses enfants   5: Générer un JSON      6: Vérifier l'emplacement de la carte
Tapez le numéro de commande que vous souhaitez effectuer :
```
Changer de carte active: 

```
Maison
└──Salon
   └──Canapé

Sur quelle carte souhaitez-vous vous placer ?

Si vous ne voulez sélectionner aucune carte, entrez comme valeur "x"
```

Supprimer une carte et ses enfants:

```
Maison
└──Salon
   └──Canapé

Quelle carte souhaitez-vous supprimer ?
```
```
La carte "Salon" a bien été supprimée

Maison

1: Ajouter une carte    2: Afficher toutes les cartes   3: Changer de carte
4: Supprimer une carte et ses enfants   5: Générer un JSON      6: Vérifier l'emplacement de la carte
Tapez le numéro de commande que vous souhaitez effectuer :
```
Création du string JSON:
```
Maison
├──Salon
|   ├──Canapé
|   └──Télé
└──Cuisine
   ├──Frigo
   └──Table à manger
Jardin
├──Arbres
└──Poubelle

1: Ajouter une carte    2: Afficher toutes les cartes   3: Changer de carte
4: Supprimer une carte et ses enfants   5: Générer un JSON      6: Vérifier l'emplacement de la carte
Tapez le numéro de commande que vous souhaitez effectuer :
```
```
[{"title":"Maison","level":0,"children":[{"title":"Salon","level":1,"children":[{"title":"Canapé","level":2,"children":[]},{"title":"Télé","level":2,"children":[]}]},{"title":"Cuisine","level":1,"children":[{"title":"Frigo","level":2,"children":[]},{"title":"Table à manger","level":2,"children":[]}]}]},{"title":"Jardin","level":0,"children":[{"title":"Arbres","level":1,"children":[]},{"title":"Poubelle","level":1,"children":[]}]}]

1: Ajouter une carte    2: Afficher toutes les cartes   3: Changer de carte
4: Supprimer une carte et ses enfants   5: Générer un JSON      6: Vérifier l'emplacement de la carte
Tapez le numéro de commande que vous souhaitez effectuer :
```

## Autres commandes
Il y a d'autres commande comme vérifier l'emplacement des cartes pour trouver le chemin d'une carte en particulier.

Afficher toutes les cartes pour avoir l'arborescence au complet.

Le programme n'est pas totalement terminé. Il y a des cas d'erreur que l'utilisateur peut effectuer qui n'a pas été traité dans le code par manque de temps. Également des cas où la console n'est pas vidée pour garder un affichage épuré.

La partie JSON n'est pas totalement terminée. Il génère un string JSON correct, mais il n'y a pas de création de fichier pour celui-ci et il n'y a également pas d'import de fichier JSON pour la création de carte.

Je n'ai pas eu assez de temps pour coder toutes ses fonctionnalités. La consigne de temps a été largement dépassé, car je voulais tout de même fournir un code qui soit propre et correct.

Je me suis beaucoup amusé à coder ce petit programme, c'est aussi pour cette raison que j'ai dépassé le temps qui était exigé.
