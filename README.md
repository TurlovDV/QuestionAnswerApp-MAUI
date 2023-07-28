
<br>
<h1 align='Center'>QuestionAnswer</h1>

 <img height='16px' src='C:\QuestionAnswerProject\QuestionAnswer\ResourceGit.QuestionAnswer.png'>


<small align='Center'>Разарботать клиент-серверное мобильное приложение, где пользователь может как задать вопрос, так и ответить в виде аркадной игры</small>


</br>

<strong>Пользователь может:</strong>

+ Авторизоваться/зарегистрирооваться в приложении
+ Задать вопрос
+ Ответить на любой вопрос
+ Оставить комменатрий на ответ
+ Поставить лайк/дизлайк

</br>

<strong>Предварительный дизайн проекта</strong> [Figma](https://www.figma.com/file/32H5HMQlV0gsIIRjr0wspj/Untitled?type=design&node-id=0%3A1&mode=design&t=g9bvcUyDfNHzowWG-1)

---

<h1>Реализация</h1>



Использумый стэк в решении задачи:

+ ![PyLoaded](https://img.shields.io/badge/.NET-7-purple)
   + <img height='16px' src='https://img.shields.io/badge/C%23-11-red'>
+ ![PyLoaded](https://img.shields.io/badge/MAUI-purple)
   + <img height='16px' src='https://img.shields.io/badge/AUTO_MAPPER-red'>
   + <img height='16px' src='https://img.shields.io/badge/XAML-orange'>
   + <img height='16px' src='https://img.shields.io/badge/TOOLKIT_MVVM-orange'>
+ ![PyLoaded](https://img.shields.io/badge/ASP_.NET_Core-7-purple)
     + <img height='16px' src='https://img.shields.io/badge/POSTMAN-red'>  
     + <img height='16px' src='https://img.shields.io/badge/JWTBearer-green'>
+ ![PyLoaded](https://img.shields.io/badge/MS_SQL-you_like-yellow)
   + <img height='16px' src='https://img.shields.io/badge/SSMS-19-red'>
   + <img height='16px' src='https://img.shields.io/badge/SQL-gree'>
+ ![PyLoaded](https://img.shields.io/badge/MSUnit-blue)

<h3>Оссобенности</h3>

<small>Применение ахитектурного подхода MVVM</small></br>
<small>Аутентификация с помощью JWT-токенов (Access and Refresh)</small>

---

<h3>Разделение решения на проекты:</h3>

+ Tests
+ QuestionAnswer.Api
+ QuestionAnswer.Api.Client
+ QuestionAnswer.DTO
+ QuestionAnswer.Mobile
+ QuestionAnswer.ADO

<strong>Tests</strong></br>
<small>Проект MSUnit с тестами</small>

<strong>QuestionAnswers.Api</strong></br>
<small>Проект ASP .NET Core REST API</small>

<strong>QuestionAnswers.Api.Client</strong></br>
<small>Проект реализующий интефейс работы клиента с Web Api</small>

<strong>QuestionAnswers.DTO</strong></br>
<small>Модели дынных для переадачи между клиентом и сервером</small>

<strong>QuestionAnswers.Mobile</strong></br>
<small>Проект MAUI</small>

<strong>QuestionAnswers.ADO</strong></br>
<small>Проект реализующий интефейс работы с базой данных MSSQL</small>

---

<h3>Итоговое решение</h3>




	


