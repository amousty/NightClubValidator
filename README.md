# NightClubValidator
D�veloppement d�une WEB API (.NET ou .NET Core) d�une gestion de cartes de membres d'une bo�te de nuit.

# Requirements
## Features
1.  Cr�ation d'un membre :
	- Enregistrement par carte de membre + carte d'identit�.
	- Lors de la cr�ation, v�rifier si la personne est �ligible.
> [POST] /api/Members
2. Mettre � jour les informations d'un membre.
> [PUT] /api/Members/id
3. Capacit� de blacklister un membre sur une dur�e limit�e
> [PUT] /api/MemberCards/id
4. Capacit� de voir si le membre a �t� blacklist�.
> [GET] /api/Members/id

## Contrainte business
1. La cr�ation d'un membre n�cessite :
	- une adresse email OU un num�ro de t�l�phone
	- Une carte d'identit� VALIDE.
2. Une carte de membre est seulement constitu�e d'un identifiant unique (qui provient, par ex, d'un QRCode).
3. La personne doit avoir > 18 ans.
4. Une carte d'identit� est constitu�e d'informations suivantes :
	- Nom
	- Pr�nom
	- Date de naissance
	- Num�ro de registre national au format xxx.xx.xx-xxx-xx
	> Le format utilis� dans cette API est xx.xx.xx-xxx-xx
	- Date de validit�
	- Date d'expiration
	- Num�ro de carte
5. Une carte d'identit� est unique dans le syst�me.
6. Un membre peut s'enregistrer plusieurs fois avec des cartes de membres diff�rentes.
	- Perte de la carte de membre
	- Carte d'identit� expir�e ou renouvel�e.

## Contraintes techniques
1. WEB API REST (JSON)
2. Architecture la plus maintenable possible.
3. EF6 ou **EF Core** ou NHibernate
4. Avoir la possibilit� d�utiliser une base de donn�es **en m�moire** et SQLServer (via LocalDB ou SQLExpress) sans trop modifier le code.
5. Faire quelques tests unitaires seulement si n�cessaire.
> Unit test fait via POSTMAN
7. La solution doit compiler.
8. L�API doit r�pondre via des requ�tes POSTMAN
9. Github.

# Diagramme de classe
![ClassDiagram NightCLubValidator](https://ibb.co/D5ThXGC)