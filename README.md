# Docker

# Conversión de Longitud - API en .NET

He optado por .NET para la implementación de esta API, ya que es una tecnología moderna y coincide con el lenguaje que utilizo en mi trabajo, lo cual aporta valor a mi desarrollo profesional.

## Descripción

El endpoint principal permite convertir una cantidad ingresada por el usuario de una unidad de longitud a otra. La API valida que:
- Las unidades de origen y destino sean válidas.
- La cantidad sea un número positivo.

### Endpoint

- **Swagger:** [http://localhost:5824/swagger/index.html](http://localhost:5824/swagger/index.html)
- **URL:** `http://localhost:5824/api/conversion/longitud`
- **Método HTTP:** `GET`

Se encarga de convertir una cantidad ingresada por el usuario de una unidad de longitud a otra, validando las unidades de origen y destino y asegurando que la cantidad sea positiva.

## Parámetros

| Nombre         | Tipo    | Ubicación | Descripción                                                                                                                     |
|----------------|---------|-----------|---------------------------------------------------------------------------------------------------------------------------------|
| `cantidad`     | double  | Query     | La cantidad a convertir. Debe ser un número positivo.                                                                           |
| `unidadOrigen` | String  | Query     | La unidad de origen. Debe ser una de las siguientes: kilometro, hectometro, decametro, metro, decimetro, centimetro, milimetro. |
| `unidadDestino`| String  | Query     | La unidad de destino. Debe ser una de las siguientes: kilometro, hectometro, decametro, metro, decimetro, centimetro, milimetro.|

---

## Ejemplos de Solicitudes

### 200 OK

La conversión se realizó exitosamente. Se devuelve un objeto JSON con la cantidad original, las unidades de origen y destino, y el resultado de la conversión.

**Request:**  
`http://localhost:5824/api/conversion/longitud?cantidad=524&unidadOrigen=metro&unidadDestino=kilometro`

**Response:**  
```json
{
  "cantidad": 524,
  "unidadOrigen": "metro",
  "unidadDestino": "kilometro",
  "resultado": 0.524
}
```

### 400 Bad Request

La solicitud contiene errores de validación. Esto ocurre si:
1.	La cantidad es menor o igual a 0.
2.	Las unidades de origen o destino no son válidas.


**Request:**  
`http://localhost:5824/api/Conversion/longitud?cantidad=0&unidadOrigen=metro&unidadDestino=kilometro`

**Response:**  
```json
{
    "error": "La cantidad debe ser un número positivo."
}
```

**Request:**  
`http://localhost:5824/api/Conversion/longitud?cantidad=524&unidadOrigen=metr&unidadDestino=kilometro`


**Response:**  
```json
{
  "error": "Las unidades deben ser 'kilometro', 'hectometro', 'decametro', 'metro', 'decimetro', 'centimetro', o 'milimetro'."
}
```

**Comandos a ejecutar**

Construcción y Ejecución del Contenedor:
1. Construir la imagen de Docker: docker build -t conversionlongitudapi .
2. Ejecutar el contenedor: docker run -d -p 5824:5024 --name conversionlongitud conversionlongitudapi



# Kubernetes

Para implementar y escalar la aplicación de forma eficiente, Kubernetes se presenta como una solución robusta y adecuada debido a sus características de orquestación de contenedores. Kubernetes facilita la automatización de la implementación, el escalamiento y la gestión de aplicaciones contenedorizadas, lo que lo convierte en una opción ideal para manejar aplicaciones como la API ConvertirLongitud. A través de la creación de pods, servicios y un sistema de escalamiento automático, Kubernetes garantiza la alta disponibilidad, confiabilidad y capacidad de respuesta ante picos de tráfico.
En este trabajo, se abordará cómo configurar la infraestructura de Kubernetes para ejecutar esta aplicación, optimizando el rendimiento, la escalabilidad y la resiliencia. Para lograr esto, se ha configurado un Deployment con 3 réplicas, lo que asegura la alta disponibilidad y la distribución de la carga entre varios pods. Además, se ha implementado un Service de tipo NodePort para exponer la aplicación al mundo exterior, permitiendo el acceso a través de un puerto específico en el nodo del clúster.
Se evaluarán posibles ajustes en la arquitectura y las tecnologías utilizadas para aprovechar aún más las ventajas de Kubernetes, como el uso de escalado automático de pods o la implementación de almacenamiento persistente en un entorno distribuido. A pesar de que en esta etapa no se realizan cambios en la lógica de la aplicación, la infraestructura Kubernetes está configurada para asegurar que la API sea capaz de manejar diferentes cargas de trabajo de manera eficiente.



## Arquitectura Propuesta


### Cómputo

- Deployment con 3 réplicas.
- Configuración de límites de recursos para optimizar el uso de CPU y memoria.

Archivo YAML - Deployment:

https://github.com/YoelSolca/ConvertirLongitud/blob/master/ConvertirLongitud/deployment.yaml


### Red
Exposición del servicio con un NodePort para entornos locales.

Archivo YAML - Service:

https://github.com/YoelSolca/ConvertirLongitud/blob/master/ConvertirLongitud/service.yaml


**Comandos a ejecutar**


Construcción y Ejecución del Contenedor:
1. Aplicar configuración de Kubernetes: 
   - kubectl apply -f deployment.yaml
   - kubectl apply -f service.yaml
