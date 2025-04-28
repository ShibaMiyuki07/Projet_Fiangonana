🙏 Projet Fiangonana
-----------
Projet_Fiangonana est une application web développée en ASP.NET Core (C#) qui permet de gérer les membres et les dons d'une église. Elle vise à fournir un outil simple et efficace pour les responsables d’églises afin de centraliser les informations essentielles.

✨ Fonctionnalités actuelles
----------
👥 Gestion des membres : ajout, modification, suppression et affichage des membres de l’église

💰 Suivi des dons : enregistrement des dons avec détails (montant, date, donateur)

🔧 Technologies utilisées
---------
Backend : ASP.NET Core MVC (C#)

Frontend : Razor Pages, HTML, CSS, Bootstrap

Base de données : MySQL ou SQL Server (configurable)

🛠️ Installation
---------

Cloner le projet

```sh
git clone https://github.com/ShibaMiyuki07/Projet_Fiangonana.git
cd Projet_Fiangonana
```

Ouvrir dans Visual Studio

Fichier solution : Projet_Fiangonana.sln

Configurer la base de données

Modifier la chaîne de connexion dans appsettings.json :

```sh
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=fiangonana_db;user=root;password=your_password"
}
```
Appliquer les migrations
```sh
dotnet ef database update
```
Lancer l’application

```sh
dotnet run
```
L'application sera accessible à : http://localhost:5000 ou https://localhost:5001

📄 Licence
-----------
Ce projet est open-source sous licence MIT.
