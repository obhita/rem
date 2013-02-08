select 
	v.VisitTemplateKey,
	v.Name as VisitName,
	at.Name as ActivityName
from 
	VisitModule.VisitTemplate v
	join VisitModule.VisitTemplateActivityType vat on v.VisitTemplateKey = vat.VisitTemplateKey
	join VisitModule.ActivityTypeLkp at on vat.ActivityTypeLkpKey = at.ActivityTypeLkpKey
