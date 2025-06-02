const brandSelectInput = document.querySelector("#BrandId");
brandSelectInput.onchange = async (e) => {
    const brandId = e.target.value;
    try {

        const numberId = Number(brandId);
        //Lance la requête afin de récupérer les catégories (filtres) depuis l'api
        const response = await fetch('/Vehicles/Models/' + numberId);

        //Convertit la réponse au format json
        const models = await response.json();

        // Supprime les options existantes
        const modelSelectInput = document.querySelector("#ModelId")
        for (let i = modelSelectInput.options.length - 1; i >= 0; i--) {
            modelSelectInput.remove(i);
        }

        if (models.length === 0)
            return;

        //Ajoute les options
        for (let model of models) {
            const modelOption = document.createElement("option");
            modelOption.value = model.id;
            modelOption.text = model.value;
            modelSelectInput.add(modelOption, null);
        }
    } catch (e) {
        console.error("Impossible de récupérer les filtres :", e);
    }
}

const fileInput = document.querySelector("#File");
fileInput.onchange = (e) => {
    const file = e.target.files[0];
    if (file) {
        const fileName = file.name;
        
        const displayText = document.querySelector("#fileDisplayName");
        displayText.textContent = `${fileName}`;
        const displayTextInput = document.querySelector("#fileDisplayNameHideInput");
        if (displayTextInput)
            displayTextInput.value = fileName;
    }
};