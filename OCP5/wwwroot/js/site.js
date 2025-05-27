const brandSelectInput = document.querySelector("#BrandId");
brandSelectInput.onchange = async (e) => {
    const brandId = e.target.value;
    await GetFiltersFromApiAsync(brandId);
}

async function GetFiltersFromApiAsync(id) {
    try {
        
        const numberId = Number(id);
        //Lance la requête afin de récupérer les catégories (filtres) depuis l'api
        const response = await fetch('/Vehicles/Models/' + numberId);

        //Convertit la réponse au format json
        const models = await response.json();

        // Crée un bouton pour la catégorie "Tous" et l'ajoute à l'élément "filterList".
        const modelSelectInput = document.querySelector("#ModelId")
        for (let i = modelSelectInput.options.length - 1; i >= 0; i--) {
            modelSelectInput.remove(i);
        }

        //S'il n'y a aucune catégorie à charger alors on sort de la fonction
        if (models.length === 0)
            return;

        for (let model of models) {
            const modelOption = document.createElement("option");
            modelOption.value = model.id;
            modelOption.text = model.value;
            modelSelectInput.add(modelOption, null);
        }
    } catch (e) {
        // Affiche un message d'erreur si la récupération des filtres échoue.
        console.error("Impossible de récupérer les filtres :", e);
    }
}