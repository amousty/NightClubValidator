# NightClubValidator
Développement d’une WEB API (.NET ou .NET Core) d’une gestion de cartes de membres d'une boîte de nuit.

# Requirements
## Features
1.  Création d'un membre :
	- Enregistrement par carte de membre + carte d'identité.
	- Lors de la création, vérifier si la personne est éligible.
> [POST] /api/Members
2. Mettre à jour les informations d'un membre.
> [PUT] /api/Members/id
3. Capacité de blacklister un membre sur une durée limitée
> [PUT] /api/MemberCards/id
4. Capacité de voir si le membre a été blacklisté.
> [GET] /api/Members/id

## Contrainte business
1. La création d'un membre nécessite :
	- une adresse email OU un numéro de téléphone
	- Une carte d'identité VALIDE.
2. Une carte de membre est seulement constituée d'un identifiant unique (qui provient, par ex, d'un QRCode).
3. La personne doit avoir > 18 ans.
4. Une carte d'identité est constituée d'informations suivantes :
	- Nom
	- Prénom
	- Date de naissance
	- Numéro de registre national au format xxx.xx.xx-xxx-xx
	> Le format utilisé dans cette API est xx.xx.xx-xxx-xx
	- Date de validité
	- Date d'expiration
	- Numéro de carte
5. Une carte d'identité est unique dans le système.
6. Un membre peut s'enregistrer plusieurs fois avec des cartes de membres différentes.
	- Perte de la carte de membre
	- Carte d'identité expirée ou renouvelée.

## Contraintes techniques
1. WEB API REST (JSON)
2. Architecture la plus maintenable possible.
3. EF6 ou **EF Core** ou NHibernate
4. Avoir la possibilité d’utiliser une base de données **en mémoire** et SQLServer (via LocalDB ou SQLExpress) sans trop modifier le code.
5. Faire quelques tests unitaires seulement si nécessaire.
> Unit test fait via POSTMAN
7. La solution doit compiler.
8. L’API doit répondre via des requêtes POSTMAN
9. Github.

# Diagramme de classe
![ClassDiagram NightCLubValidator](https://ibb.co/D5ThXGC)