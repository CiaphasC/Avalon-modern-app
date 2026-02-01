# Avalon Modern App 🚀

Una aplicación de escritorio moderna y elegante construida con **Avalonia UI** y **.NET 9**. Este proyecto demuestra una arquitectura limpia y modular, implementando características clave como autenticación, navegación, gestión de contactos y simulación de envío de SMS.

![Avalonia Logo](https://github.com/avaloniaui/avalonia/blob/master/assets/avalonia-logo.ico)

---

## ✨ Características Principales

*   **Autenticación de Usuarios:** Sistema de inicio de sesión seguro simulado.
*   **Dashboard Interactivo:** Vista general con estadísticas y accesos rápidos.
*   **Gestión de Contactos:**
    *   Listado de contactos con búsqueda y filtrado.
    *   Diálogo modal para agregar nuevos contactos.
    *   Persistencia de datos en archivos JSON local.
*   **Sistema de SMS:**
    *   Interfaz para redactar y "enviar" mensajes SMS.
    *   Selección múltiple de destinatarios.
    *   Validación de entradas.
*   **Arquitectura MVVM:** Implementación limpia utilizando el patrón Model-View-ViewModel.
*   **Inyección de Dependencias:** Uso de contenedores DI para servicios y repositorios.
*   **Diseño Moderno:** Interfaz de usuario fluida y reactiva.

## 🛠️ Tecnologías Utilizadas

*   [Avalonia UI](https://avaloniaui.net/)
*   C# / .NET 9
*   CommunityToolkit.Mvvm
*   System.Text.Json (Persistencia)

## 📂 Estructura del Proyecto

El proyecto sigue una arquitectura organizada en capas:

*   **Core:** Entidades de dominio e interfaces (`Core/Entities`, `Core/Interfaces`).
*   **Infrastructure:** Implementación de servicios y acceso a datos (`Infrastructure/Persistence`, `Infrastructure/Services`).
*   **UI/Views:** Componentes visuales y ventanas (`Views/*.axaml`).
*   **ViewModels:** Lógica de presentación y estado (`ViewModels/*.cs`).
*   **Services:** Lógica de negocio específica de la aplicación.

## 🚀 Cómo Ejecutar

1.  **Clonar el repositorio:**
    ```bash
    git clone https://github.com/CiaphasC/Avalon-modern-app.git
    cd Avalon-modern-app
    ```

2.  **Restaurar dependencias:**
    ```bash
    dotnet restore
    ```

3.  **Ejecutar la aplicación:**
    ```bash
    dotnet run --project AvaloniaModernApp
    ```

## 👤 Credenciales de Prueba

Para acceder a la aplicación, puedes usar las siguientes credenciales predeterminadas (definidas en `users.json`):

*   **Usuario:** `admin`
*   **Contraseña:** `password`

---

Desarrollado con ❤️ usando Avalonia UI.