# OCP5
Dépôt du projet 5 du parcours Développeur Back-End .NET. 

Ce projet possède une base de données intégrée qui sera créée lorsque l’application sera exécutée pour la première fois. Pour la créer correctement, vous devez satisfaire aux prérequis ci-dessous et mettre à jour les chaînes de connexion pour qu’elles pointent vers le serveur MSSQL qui est exécuté sur votre PC en local.

**Prérequis** : 
- Système de gestion de base de données relationnelle : **Microsoft SQL Server 2022 Express** *(MSSQL Server Express 2022)*
- Outils d'administration de base de données : **Microsoft SQL Server Management Studio** *(SSMS)*

**Téléchargements** :

MSSQL : https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads

SSMS : https://learn.microsoft.com/fr-fr/ssms/install/install

**Remarques** : 

- MSSQL Server n'est disponible que pour **Windows** et les systèmes d'exploitations basés sur **LINUX tels qu'Ubuntu, Fedora, etc**. Si vous utilisez un système d'exploitation non compatible, vous devrez utiliser un conteneur Docker avec MSSQL Server ou exécuter une machine virtuelle Windows.
- Le type d’authentification est « **Authentification Windows** »
- Les versions antérieures de MSSQL Server devraient fonctionner sans problème, mais elles n’ont pas été testées.
- Si SSMS n'est pas disponible sur votre machine ou si vous le souhaitez, vous pouvez utiliser un autre outil d'administration de base de données, tel que DBeaver, le plug-in MSSQL sur Visual Studio Code, le plug-in Database sur les IDE JetBrains ou un autre outil de votre choix. Cependant, il est recommandé d'utiliser SSMS pour ce projet, car il peut être plus facile à utiliser.

**Attention** :

- MSSQL Server est la base de données utilisée dans ce projet. Si vous utilisez un autre système de gestion de base de données relationnelle, tel que MySQL, PostgreSQL ou SQLite, vous devrez adapter le code pour qu'il fonctionne avec ce système. Cela peut nécessiter des modifications importantes du code et des dépendances.
- La chaîne de connexion définie dans le projet est configurée pour MSSQL Server Express 2022, il se peut donc que vous deviez l'adapter si vous utilisez une autre version, édition de MSSQL Server ou en fonction des modifications apportées à votre instance lors de son installation.
- Si vous avez des difficultés à vous connecter, essayez d’abord de vous connecter à l’aide de Microsoft SQL Server Management Studio (assurez-vous que le type d’authentification est « Authentification Windows »), ou consultez le site https://doc.milestonesys.com/latest/en-US/system/failover/management_server/fms_sqlserverinstancename.htm#:~:text=Open%20the%20Start%20menu%2C%20and,SQL%20Server%20%5BDisplay%20name%5D *(En anglais, pour Windows seulement)*.

*Dans le projet OCP5, ouvrez le fichier appsettings.json.*

Vous avez la section ConnectionStrings qui définit la chaîne de connexion pour la base de données utilisée dans cette application.

      "ConnectionStrings":
      {
        "DefaultConnection": "Server=localhost\SQLEXPRESS;Database=VehiculeExpress;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
      }

**DefaultConnection** - chaîne de connexion à la base de données de l’application.
