#Использовать json

Функция Index() Экспорт
	
	Перем Модель;

	ФайлНастроек = Новый Файл(ОбъединитьПути(КаталогПрограммы(), "appsettings.json"));
	Если ФайлНастроек.Существует() Тогда
		Парсер = Новый ПарсерJSON();
		Текст = Новый ТекстовыйДокумент;
		Текст.Прочитать(ФайлНастроек.ПолноеИмя);
		Данные = Парсер.ПрочитатьJSON(Текст.ПолучитьТекст());

		Модель = Данные;
		
	КонецЕсли;

	
	Ответ = Новый РезультатДействияСтраница();
	ДанныеПредставления        = Новый СловарьДанныхПредставления();
	ДанныеПредставления.Модель = Модель;
	Ответ.ДанныеПредставления  = ДанныеПредставления;

	Возврат Ответ;
КонецФункции

Функция Environment() Экспорт

	Ответ = Новый РезультатДействияСтраница();
	ДанныеПредставления        = Новый СловарьДанныхПредставления();
	ДанныеПредставления.Модель = ПеременныеСреды();
	Ответ.ДанныеПредставления  = ДанныеПредставления;

	Возврат Ответ;

КонецФункции