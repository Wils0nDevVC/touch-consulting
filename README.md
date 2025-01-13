#Levantar Proyecto

##Appseting.Development.json
  - En ConnectionStrings
  `"ConnectionStrings": {
  "DB_TOUCHCONSULTING": "Tu-Cadena-de-conexion-SQL-SERVER"
}`
` "MailSettings": {
   "MailFrom": "tucorreo@proveedor.com",
   "MailPassword": "tupasswordapps_creado_en_google_u_otro_proveedor"
 },
`

##Contrucción de BD.
- Crear una migración
  ` dotnet ef migrations add NewMigration `
- Ejecutar la migración
 `dotnet ef database update`

##Por ultim
- Dale Inciar desde Visual Studio 
