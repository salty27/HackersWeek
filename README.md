# 🚀 HackersWeekAPI

👋 Hola! Si estás en este taller, se supone que es porque quieres aprender a crear tu propia **API** y desplegarla en **Azure** ☁️ usando **GitHub Actions**. La realidad es que va a ser bastante simple, pero tienes que saber ciertas cosas que se van a explicar en este README.md.

## 🕵️‍♂️ ¿Cómo puedo saber si funciona mi api?

Lo más simple es entrar en el enlace que hayas creado para tu app en **Azure** y, si todo está bien, lo normal es que veas `URL SHORTENER` en tu navegador. Aun así, si eres un poco más ambicioso y quieres estrujar todas las funcionalidades (que sería lo suyo), puedes probar a hacer una petición usando curl en tu consola:
```bash
curl -X POST https://TU_APP.azurewebsites.net/urls \
  -H "Content-Type: application/json" \
  -d '{"url": "[https://www.githubcommunity.es](https://www.githubcommunity.es)"}'
```

O puedes acceder a la documentación dinámica con ```[enlace de la API]/scalar```

## 🔐 ¿Cómo puedo obtener los secretos de Azure?

Como ya deberías saber, para que tu **API** funcione correctamente, vas a necesitar tres valores muy importantes: el `subscriptionId`, el `tenantId` y el `clientId`. Normalmente, estos se pueden conseguir en el apartado de **Microsoft Entra ID** y viendo los datos que te da el Dashboard de tu **App Service**, pero ¿qué te creías que iba a ser tan fácil? La UMA siempre hace de las suyas y esta vez nos prohíbe el acceso a ello. Aunque podemos usar un **trukele** (probablemente, un fallo de seguridad garrafal) para conseguir esta info tan preciada.

Por lo tanto, entra en [Azure CloudShell](https://portal.azure.com/#cloudshell) y ejecuta este comando para obtener el `subscriptionId` y el `tenantId`:
```bash
az account show --query "{subscriptionId:id, tenantId:tenantId}" -o json
```
Y por último, ejecuta este comando cambiando `EL_NOMBRE_DE_TU_SERVICIO` por el nombre de tu servicio. No me seas melón 🍈:

```bash
az ad sp list --display-name "EL_NOMBRE_DE_TU_SERVICIO" --query "[0].{clientId:appId}" -o json
```

Por nuestra parte, esto es todo, por lo que si hay algo que no sepas hacer durante el taller, tranquilo, no eres un cenutrio (que igual sí), pero lo más probable es que hayamos obviado u olvidado eso que te inquieta, entonces levanta tu mano y pregúntanos lo que quieras. Happy codding!
