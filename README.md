# domicats
Site web d'affichage et de sélection de chats<br />
Technologie : .NET CORE 2.2.108 SDK (compatible VS2017), Vue.js et TypeScript<br />
<p>
Application en cours de développement....
</p>
<p>
Scaffolding via le projet : https://www.nuget.org/packages/AspNetCore.Vue.TypeScript.Template/
</p>

<h2>Stack</h2> 
<ul>
<li><a href="https://www.microsoft.com/net" target="_blank">ASP.NET Core 2.2</a></li> 
<li><a href="https://vuejs.org/" target="_blank">Vue</a></li> 
<li><a href="https://router.vuejs.org/" target="_blank">Vue router</a></li> 
<li><a href="https://vuex.vuejs.org/" target="_blank">Vuex</a></li>
<li><a href="https://www.typescriptlang.org/" target="_blank">TypeScript</a></li>
<li><a href="https://bulma.io/" target="_blank">Bulma</a></li> 
<li><a href="https://sass-lang.com/" target="_blank">Sass</a></li> 
<li><a href="https://jestjs.io/" target="_blank">Jest</a></li> 
<li><a href="https://webpack.js.org/" target="_blank">Webpack 4</a></li>
</ul>

<h2>Mon architecture</h2>
Appli_UI : appli web (Vue.js, Vuex.js)<br />
Serveur web : .Net Core ASP.NET MVC : hébergement site web (une seule page)<br />
Serveur websocket : intégré à l'appli web : communication entre client et serveur : BFF (Background For Frontback) : .Net Core<br />
BackOffice : traitement de la communication via appel à l'API et via la génération d'évènement vers tous les applis UI connectées au BFF<br />
Composant API  : accès à la base de données SQLite : DLL en .Net Core


