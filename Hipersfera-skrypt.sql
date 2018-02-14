DROP Type Punkt
GO
DROP ASSEMBLY [Punkt_a]
GO

CREATE ASSEMBLY [Punkt_a]
AUTHORIZATION [dbo]
FROM 'C:\Users\Lukasz\Documents\CLR_UDT\CLR_UDT\bin\Debug\CLR_UDT.dll'
WITH PERMISSION_SET = SAFE
GO
CREATE TYPE dbo.Punkt
EXTERNAL NAME Punkt_a.Punkt;


declare @a Punkt, @b Punkt
set @a=CAST('1,1' as Punkt)
set @b=CAST('6,6' as Punkt)

select @a.ToString() as 'Punkt', @b.ToString() as 'Punkt', @a.ZbiorPunktowDla(9) as 'Zbior punktow dla srodka (0,0) i wymiaru',
@a.ZbiorPunktow(3, 3,2) as 'Zbior punktow dla punktu o wspolrzednych i wymiaru',@a.ZbiorPunktowOd(@b,6) as 'Zbior punktow dla srodka b i wymiaru'

declare @a Punkt, @b Punkt
set @a=CAST('1,1' as Punkt)
set @b=CAST('6,6' as Punkt)
select @a.ToString() as 'Punkt', @b.ToString() as 'Punkt', @a.ObjetoscOd(2) as 'Objetosc hipersfery dla srodka (0,0) i wymiaru',
@a.ObjetoscDla(@b,2) as 'Objetosc hipersfery dla srodka b i wymiaru',@a.Objetosc(3, 3,2) as 'Objetosc hipersfery dla punktu o wspolrzednych i wymiaru',

declare @a Punkt, @b Punkt
set @a=CAST('1,1' as Punkt)
set @b=CAST('6,6' as Punkt)
select @a.ToString() as 'Punkt', @b.ToString() as 'Punkt',
@a.Pole(5, 5,4) as 'Powierzchnia hipersfery dla punktu o wspolrzednych i wymiaru',
@a.PoleDla(@b,5) as 'Powierzchnia hipersfery dla srodka b i wymiaru',
@a.PoleOd(7) as 'Powierzchnia hipersfery dla srodka (0,0) i wymiaru'


/*Najpierw podane s� punkt i �rodek I hipersfera, potem �rodek i punkt drugiej. Wy�wietlane s�
promienie pierwszego i drugiego. Wyliczana jest odleglosc, gdy hipersfery si� stykaj�. Obliczana jest
te� odleg�o�� mi�dzy �rodkami. Je�li jest wi�ksza to znaczy, �e si� hipersfery nie stykaj�, je�li taka
sama, to hipersfery si� stykaj�, a je�li mniejsza to hipersfery si� przycinaj�. Je�li �rodki s� takie same
i promienie hipersfer maj� tak� sam� d�ugo��, to znaczy �e hipersfery si� nachodz�. Je�li promie� pierwszej 
hipersfery jest wi�kszy od promienia drugiej, to II pierwsza hipersfera jest wewn�trz I hipersfery, a je�li
promie� drugiej jest wi�kszy, to I hipersfera jest wewn�trz II hipersfery*/
declare @a Punkt, @b Punkt, @c Punkt, @d Punkt
set @a=CAST('1,1' as Punkt)
set @b=CAST('3,3' as Punkt)
set @c=CAST('0,0' as Punkt)
set @d=CAST('3,3' as Punkt)
select @a.ToString() as 'Punkt I Hipersfery',@b.ToString() as 'Srodek II Hipersfery',
 @c.ToString() as 'Punkt II Hipersfery', @d.ToString() as 'Srodek II Hipersfery',
 @a.Promien11(@b) as 'Promien Pierwszej hipersfery',
 @a.Promien22(@c,@d) as 'Promien Drugiej hipersfery',
 @a.Styk1(@b,@c,@d) as 'Odleglosc pomiedzy srodkami hipersfer, gdy hipersfery sie stykaja',
 @a.Przecina(@b,@c,@d) as 'Odleglosc pomiedzy srodkami hipersfer'
