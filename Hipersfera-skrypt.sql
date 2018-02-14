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


/*Najpierw podane s¹ punkt i œrodek I hipersfera, potem œrodek i punkt drugiej. Wyœwietlane s¹
promienie pierwszego i drugiego. Wyliczana jest odleglosc, gdy hipersfery siê stykaj¹. Obliczana jest
te¿ odleg³oœæ miêdzy œrodkami. Jeœli jest wiêksza to znaczy, ¿e siê hipersfery nie stykaj¹, jeœli taka
sama, to hipersfery siê stykaj¹, a jeœli mniejsza to hipersfery siê przycinaj¹. Jeœli œrodki s¹ takie same
i promienie hipersfer maj¹ tak¹ sam¹ d³ugoœæ, to znaczy ¿e hipersfery siê nachodz¹. Jeœli promieñ pierwszej 
hipersfery jest wiêkszy od promienia drugiej, to II pierwsza hipersfera jest wewn¹trz I hipersfery, a jeœli
promieñ drugiej jest wiêkszy, to I hipersfera jest wewn¹trz II hipersfery*/
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
