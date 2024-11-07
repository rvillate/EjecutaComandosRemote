# Aplicación WPF para Control de Tareas en Base de Datos MySQL
![image](https://github.com/user-attachments/assets/da25d164-76bd-4363-8e07-b01b1b49501e)

## Descripción
Esta aplicación WPF interactúa con una base de datos MySQL para gestionar tareas pendientes. Dependiendo de una variable de configuración, la aplicación puede ser visible o no. Su propósito principal es conectar a la base de datos, leer registros de tareas pendientes y ejecutarlas si están marcadas como "Corriendo" o "Pendiente". Además, muestra mensajes en pantalla y ejecuta archivos CMD relacionados con las tareas.

## Requisitos

### Hardware
- PC con Windows (mínimo: Windows 7)

### Software
- Microsoft Visual Studio
- .NET Framework 4.5 o superior
- MySQL Server (y acceso a una base de datos)
- MySQL.Data (paquete NuGet)

## Instalación

1. **Clonar el repositorio:**

   ```bash
   git clone https://github.com/usuario/repo.git

2. **Abrir el proyecto en Visual Studio:**

- Abre el archivo .sln en Visual Studio.
- Asegúrate de tener todas las dependencias necesarias (como MySQL.Data) instaladas mediante NuGet.

3. **Configurar la base de datos:**
- Crea una base de datos MySQL llamada ReportesYHerramientas_202207042053.**
- Asegúrate de tener una tabla con el siguiente esquema:

```sql
CREATE TABLE `ReportesYHerramientas_202207042053` (
    `id` INT AUTO_INCREMENT PRIMARY KEY,
    `estado` VARCHAR(50),
    `solicitud` TEXT,
    `hora_solicitud` DATETIME,
    `argumentos_1` VARCHAR(255),
    `argumentos_2` VARCHAR(255),
    `argumentos_3` VARCHAR(255),
    `log_estado` TEXT,
    `id_app` VARCHAR(255)
);
```
4. **Configuración de conexión:**
- Modifica las credenciales de conexión en el archivo de configuración de la base de datos dentro del código, en la clase MyDBSQL (servidor, puerto, base de datos, usuario y contraseña).

# Funcionamiento
1. Conexión a la Base de Datos: La aplicación se conecta a la base de datos MySQL usando las credenciales especificadas. Si la conexión es exitosa, cambia el color de estado a verde y empieza a leer los registros con estado "Corriendo" o "Pendiente" para ejecutar las tareas asociadas.

2. Ejecución de Tareas: Si se encuentra una tarea pendiente, la aplicación lee los parámetros de la tarea (argumentos_1, argumentos_2, etc.) y ejecuta el programa correspondiente (como archivos .cmd). Después de la ejecución, actualiza el estado de la tarea en la base de datos a "Terminado".

3. Visibilidad de la Aplicación: La visibilidad de la aplicación depende de la configuración visible_app. Si está configurada como false, la ventana de la aplicación se oculta, pero la ejecución de las tareas sigue funcionando en segundo plano.

4. Interfaz de Usuario: La interfaz de usuario muestra el estado de la conexión a la base de datos, mensajes de ejecución de tareas y un indicador visual de estado (círculos de colores) que cambia dependiendo de si la aplicación está ejecutando tareas.

# Funcionalidades
- Conexión a la base de datos MySQL.
- Ejecución de tareas programadas basadas en registros de la base de datos.
- Actualización de registros en la base de datos (de "Pendiente" a "Terminado").
- Interfaz visual para mostrar el estado de la aplicación y las tareas.
- Ejecutar archivos CMD relacionados con las tareas.
- Mostrar mensajes informativos en la pantalla.
# Estructura del Proyecto
- MainWindow.xaml.cs: Controla la interfaz de usuario y la lógica de la aplicación.
- MyDBSQL.cs: Maneja las conexiones a la base de datos y las consultas SQL.
- Extras: Funciones adicionales para manejo de logs y herramientas del sistema.
# Dependencias
- MySQL.Data: Paquete NuGet para interactuar con bases de datos MySQL.
- System.Threading: Para la ejecución asíncrona de tareas.
- System.Windows: Para crear la interfaz de usuario en WPF.

# Cómo Usar
1. Iniciar la Aplicación:
- La aplicación se inicia automáticamente al abrirse. Si la base de datos está accesible, se conectará y comenzará a monitorear las tareas pendientes.

2. Monitorización de Estado:
- La aplicación actualiza su interfaz con el estado de las tareas y de la base de datos. Los círculos de colores indican el estado de la conexión y las tareas en progreso.

3. Ejecutar Archivos CMD:
- Las tareas en la base de datos pueden incluir la ejecución de scripts .cmd. Asegúrate de que los scripts estén configurados correctamente en los registros de la base de datos.

# Contribuciones
Si deseas contribuir al proyecto, por favor realiza un fork y crea una nueva rama. Abre un Pull Request para que revisemos tus cambios.

# Licencia
Este proyecto está bajo la Licencia MIT. Ver el archivo LICENSE para más detalles.

```css
Este `README.md` cubre la descripción general, los requisitos, la instalación y las funcionalidades del proyecto. Puedes adaptarlo según sea necesario para tu caso.
```
