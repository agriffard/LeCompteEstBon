# PRD — Le compte est bon (Blazor WASM)

## 1. Contexte et objectif
Créer une application cliente Blazor WebAssembly pour jouer au jeu télévisé "Le compte est bon". L'utilisateur reçoit 7 nombres et un nombre cible. Il doit composer le plus proche possible du nombre cible en utilisant chaque nombre au maximum une fois et les opérations arithmétiques élémentaires (+, −, ×, ÷). L'application proposera un tirage aléatoire et un solveur automatique.

## 2. Utilisateurs cibles
- Joueurs occasionnels qui veulent s'entraîner.
- Enseignants/étudiants pour exercices de calcul mental.
- Développeurs souhaitant étudier algorithmes de recherche/exhaustivité.

## 3. Valeur métier
- Outil d'entraînement et de divertissement.
- Support éducatif pour calcul mental et logique.
- Démo technique pour Blazor WASM et algorithme de recherche.

## 4. Fonctionnalités principales
### 4.1 Tirage
- Bouton "Tirage aléatoire".
- Génération de 7 nombres selon règles classiques (par ex. 1–10 petits; 25,50,75,100 grands) ou configuration.
- Génération d'un nombre cible entre 100 et 999 (configurable).

### 4.2 Interface de jeu
- Affichage clair des 7 nombres et du nombre cible.
- Zone d'assemblage pour créer une expression (drag & drop ou clic).
- Historique des opérations partielles (facultatif).
- Validation d'expressions (usage unique des nombres).
- Bouton "Vérifier" pour calculer résultat de l'expression de l'utilisateur.

### 4.3 Solveur automatique
- Bouton "Résoudre" qui lance un algorithme pour trouver une solution exacte ou la plus proche.
- Affichage de la meilleure solution trouvée et des étapes.
- Option pour afficher toutes les solutions ou arrêter après la première exacte.

### 4.4 Options et paramètres
- Choix des règles de tirage (proportions petits/grands, plage du cible).
- Limite de temps pour le solveur (pour éviter blocage UI).
- Niveau de détail des solutions affichées.

### 4.5 Accessoires UI
- Réinitialiser partie.
- Copier l'énoncé (7 nombres + cible).
- Partager lien d une partie (optionnel).

## 5. Flux utilisateur (exemples)
1. Page d'accueil. Boutons: "Tirage aléatoire", "Résoudre", Réglages.
2. Cliquer "Tirage aléatoire". Les 7 nombres et le cible s'affichent.
3. L'utilisateur compose des opérations puis clique "Vérifier".
4. Si insatisfait, clique "Résoudre" pour voir solution automatique.

## 6. Exigences fonctionnelles
- FR-F1: Générer et afficher un tirage valide.
- FR-F2: Valider qu'un nombre n'est utilisé qu'une fois.
- FR-F3: Calculer l'expression de l'utilisateur et afficher résultat.
- FR-F4: Lancer le solveur et retourner la meilleure solution trouvée.
- FR-F5: UI responsive pour desktop et mobile.

## 7. Exigences non fonctionnelles
- NFR-1: Application cliente pure (Blazor WASM). Aucune dépendance serveur obligatoire.
- NFR-2: Temps de réponse du solveur configurable; défaut 5s.
- NFR-3: Code testable et maintenable. Couverture tests unitaires pour le solveur.
- NFR-4: Licence permissive pour code (MIT par défaut).

## 8. Algorithme de résolution (spécification)
- Approche proposée: recherche exhaustive avec pruning.
  - Représenter l'état comme multiensemble de nombres disponibles.
  - À chaque étape, prendre 2 nombres a et b, appliquer les opérations valides:
    - a+b
    - a-b et b-a (si différent)
    - a*b
    - a/b et b/a si division entière ou flottante selon règles (idéalement flottante mais tolérance d'erreur).
  - Remplacer a,b par le résultat et récursion jusqu'à un seul nombre.
  - Mesurer distance absolue au cible et conserver meilleures solutions.
- Optimisations:
  - Élagage si le résultat est identique à un état déjà rencontré (hash des multiensembles).
  - Élagage symétries (commutativité) en imposant ordre sur paires.
  - Limiter profondeur et temps (timeout configuré).
  - Utiliser arithmétique rationnelle (fractions) pour éviter erreurs flottantes.
- Entrées/sorties du module:
  - Entrée: liste de 7 nombres, cible, timeout, option exact-only.
  - Sortie: solution(s) avec expression textuelle et distance au cible, temps d'exécution, nombre d'états explorés.

## 9. UI / Wireframes (texte)
- En-tête: titre + réglages.
- Colonne gauche: 7 tuiles de nombres + bouton Tirage.
- Centre: cible (grand) + zone d'édition d'expression.
- Droite: contrôles: "Vérifier", "Résoudre", historique et affichage solutions.
- Bas: log d'activité et indicateur de performance du solveur.

## 10. Critères d'acceptation
- AC-1: Tirage aléatoire génère 7 nombres et cible et s'affiche.
- AC-2: L'utilisateur peut construire une expression et la vérifier.
- AC-3: Le solveur retourne au moins une solution dans le timeout par défaut si elle existe.
- AC-4: Tests unitaires couvrant cas edge: divisions non entières, réutilisation des nombres interdite, symétries.

## 11. Plan de livraison (itérations)
- Sprint 1: Prototype UI + tirage aléatoire + affichage.
- Sprint 2: Construction d'expression utilisateur + vérification.
- Sprint 3: Implémentation du solveur exhaustif + affichage solution.
- Sprint 4: Optimisations, tests, responsive design, pack release.

## 12. Contraintes et risques
- Recherche exhaustive peut exploser le temps CPU sans pruning.
- Ergonomie de composition d'expression (drag & drop) peut être complexe sur mobile.

## 13. Annexes techniques
- Tech stack: Blazor WebAssembly, .NET 7+.
- Tests: xUnit pour logique, Playwright pour tests UI (optionnel).
- Représentation des nombres: System.Numerics.BigInteger et System.Numerics.BigRational (ou Fraction custom) pour exactitude.

---

Si vous voulez, je peux générer l'arborescence de projet, le code du solveur en C# ou les composants Blazor de base. Indiquez ce que je dois produire ensuite.

