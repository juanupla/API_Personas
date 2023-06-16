const API_PERSONAS_URL = 'https://localhost:7008/api/personas/getPersonas' //link de obtener todas las personas. ejecutá el swagger y ves la url

function listaPersonas() {
    fetch(API_PERSONAS_URL)
    
        .then((respuesta) => respuesta.json())
        .then((respuesta) => {
            if (!respuesta.ok) {
                alert("ERROR!")
                return
            }

            const cuerpoTabla = document.querySelector('tbody')

            //nombre de la lista. Correla en swagger y la ves en el json
            respuesta.listPersonas.forEach((per) => {
                const fila = document.createElement('tr')
                fila.innerHTML += `<td>${per.id}</td>` 
                fila.innerHTML += `<td>${per.nombre}</td>`
                fila.innerHTML += `<td>${per.apellido}</td>`

                cuerpoTabla.append(fila)
            });


        }).catch((err)=>{
            alert("No funciono")
        })
    

}