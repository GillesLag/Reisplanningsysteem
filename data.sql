INSERT INTO RPS.Gemeenten(Naam, Postcode) 
VALUES ('Antwerpen', '2000'), ('Brussel', '1000'), ('Leuven', '3000');

INSERT INTO RPS.Bestemmingen(Naam, GemeenteId, Huisnummer, Land, Straat) 
VALUES ('Hilton Antwerp Old Town', 1, '2', 'België', 'Beddenstraat'), ('Hotel Mercure Brussels Centre Midi', 2, '25', 'België', ' Jamarlaan');

INSERT INTO RPS.Cursussen(Naam) 
VALUES ('EHBO'), ('Basis Cursus'), ('Rijbewijs B'), ('Hoofdmonitor cusrsus');

INSERT INTO RPS.Themas(Naam) 
VALUES ('Surfen'), ('Culinair'), ('Kunst');

INSERT INTO RPS.LeeftijdsCategorieën(Naam, LeeftijdMinimum, LeeftijdMaximum) 
VALUES ('Kinderen', 6, 12), ('Jongeren', 12, 18), ('Volwassenen', 18, 60), ('Senioren', 60, 100);

INSERT INTO RPS.Gebruikers(Voornaam, Achternaam, Email, Straat, Huisnummer, GemeenteId, BasisCursus, HoofmonitorCursus, IsLid) 
VALUES ('Gilles', 'Lagrillière', 'gilleslagrilliere@gmail.com', 'Nieuwstraat', '54', 1, 1, 1, 1),
('Dries', 'Van Hool', 'driesvanhool@gmail.com', 'Italiëlei', '1', 1, 1, 0, 1);

INSERT INTO RPS.Reizen(Naam, BeginDatum, EindDatum, Prijs, ThemaId, BestemmingsId, HoofdmonitorId, LeeftijdsCategorieId) 
VALUES ('Citytrip Antwerpen', '2023-07-01', '2023-07-07', 300, 3, 1, 1, 3);

INSERT INTO RPS.Boekingen(IsMonitor, [Status], InschrijvingsDatum, GebruikerId, ReisId) 
VALUES (1, 'Betaald', '2023-06-10', 1, 1), (0, 'Betaald', '2023-06-10', 2, 1);

INSERT INTO RPS.GebruikersCursusen(GebruikerId, CursusId) 
VALUES (1, 2), (1, 4), (2, 2);