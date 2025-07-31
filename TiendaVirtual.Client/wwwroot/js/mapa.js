// wwwroot/js/mapa.js
window.maps = window.maps || {};

window.initializeMap = function (mapId, latInicial, lngInicial, dotNetRef) {
    const mapElement = document.getElementById(mapId);
    if (!mapElement) {
        console.error("Elemento del mapa no encontrado:", mapId);
        return;
    }

    // Convertir a números o usar valores por defecto si son null/undefined
    const lat = latInicial != null ? parseFloat(latInicial) : -17.7836;
    const lng = lngInicial != null ? parseFloat(lngInicial) : -63.1824;

    // Crear el mapa
    const map = L.map(mapId).setView([lat, lng], 13);

    // Añadir capa de tiles
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    // Crear marcador
    let marker = L.marker([lat, lng], { draggable: true }).addTo(map);

    // Función para actualizar la posición del marcador
    function updateMarker(lat, lng) {
        marker.setLatLng([lat, lng]);
        if (dotNetRef) {
            dotNetRef.invokeMethodAsync('OnMapClick', lat, lng);
        }
    }

    // Evento de clic en el mapa
    map.on('click', function (e) {
        updateMarker(e.latlng.lat, e.latlng.lng);
    });

    // Evento de arrastre del marcador
    marker.on('dragend', function (e) {
        const latlng = marker.getLatLng();
        if (dotNetRef) {
            dotNetRef.invokeMethodAsync('OnMapClick', latlng.lat, latlng.lng);
        }
    });

    // Guardar referencia del mapa
    window.maps[mapId] = { map: map, marker: marker, updateMarker: updateMarker };
};

window.destroyMap = function (mapId) {
    if (window.maps[mapId]) {
        window.maps[mapId].map.remove();
        delete window.maps[mapId];
    }
};