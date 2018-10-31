Feature: CreateIndividualEntrepreneur
		Я хочу добавить ИП

Background: Залогиниться и войти в главную страницу
Given Я на странице авторизации
	And Я анонимный пользователь
	When Я ввожу данные и нажимаю кнопку "Вход"
	Then Я авторизуюсь на сайте

@mytag
Scenario: Create IndividualEntrepreneur
	Given Я нажимаю на ссылку ИП
	And Перехожу на страницу ИП
	When Я нажимаю на ссылку создания 
	Then Вижу страницу создания ИП
	And Я заполняю все поля и нажимаю кнопку
	Then Я вижу страницу со списком ИП
