# TC2008B.302_TeamMultiagentes

## Situación Problema #1

### Contexto de Siutación Problema

Supongamos que somos los nuevos propietarios de 5 robots nuevos y un almacén lleno de cajas. El dueño anteior del almacén lo dejó en completo desorden, por lo que depende de nuestro robots organizar las cajas en algo parecido al orden y convertirlo en un negocio exitoso.

Nuestro trabajo consiste en modelar y desplegar la representación en 3D del mismo. El diseño y despliegue incluye:
- Modelos con materiales (colores) y texturas (usando mapeo UV):
    - Estante (con repetición de instanticas o prefabs por código).
    - Caja (con repetición de instanticas o prefabs por código).
    - Robot (con repetición de instancias o prefabs por código, al menos 5 robots).
    - Almacén (piso, paredes y puerta).
- Animación:
  - Los robots se desplazan sobre el piso del almacén, en los pasillos que forman los estantes.
  - Sin conexión son el despliegue de la simulación.
- Iluminación:
  - Fuente de luz direccional
  - Fuente de luz puntual sobre cada robot (tipo sirena). Dicha luz se mueve con cada robot. 
- Detección de colisiones básica:
  - Los robots se mueven en rutas predeterminadas.
  - Los robots se mueven con velocidad predeterminada (aleatoria).
  - Los robots comienzan a operar en posiciones predeterminadas (aleatorias.
  - Los robots detectan y reaccionan a colisiones entre ellos.
  
## Situación Problema #2
### Contexto de Situación Problema
La movilidad urbana, se define como la habilidad de transportarse de un lugar a otro1 y es fundamental para el desarrollo económico y social y la calidad de vida de los habitantes de una ciudad. Desde hace un tiempo, asociar la movilidad con el uso del automóvil ha sido un signo distintivo de progreso. Sin embargo, esta asociación ya no es posible hoy. El crecimiento y uso indiscriminado del automóvil —que fomenta políticas públicas erróneamente asociadas con la movilidad sostenible—genera efectos negativos enormes en los niveles económico, ambiental y social en México.

Durante las últimas décadas, ha existido una tendencia alarmante de un incremento en el uso de automóviles en México. Los Kilómetros-Auto Recorridos (VKT por sus siglas en Inglés) se han triplicado, de 106 millones en 1990, a 339 millones en 2010. Ésto se correlaciona simultáneamente con un incremento en los impactos negativos asociados a los autos, como el smog, accidentes, enfermedades y congestión vehicular2.

Para que México pueda estar entre las economías más grandes del mundo, es necesario mejorar la movilidad en sus ciudades, lo que es crítico para las actividades económicas y la calidad de vida de millones de personas.

Este reto te permitirá proponer una solución al problema de movilidad urbana en México, mediante un enfoque que reduzca la congestión vehicular al simular de manera gráfica el tráfico, representando la salida de un sistema multi agentes.

## Link a paquetes de unity
https://drive.google.com/drive/folders/1wV66Lq4A5PisYBKn7yfNUM7ULTr4o7rM?usp=sharing
