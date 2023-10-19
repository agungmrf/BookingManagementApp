$(document).ready(() => {
    $('#pokeTable').DataTable({
        ordering: true,
        ajax: {
            url: 'https://pokeapi.co/api/v2/pokemon?offset=0&limit=2000',
            dataSrc: 'results'

        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                data: 'name',
                render: function (data, type, row) {
                    return data.charAt(0).toUpperCase() + data.slice(1);
                }
            },
            {
                render: (data, type, row) => {
                    return `<button type="button" onclick="detail('${row.url}')" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button>`
                }
            }
        ],
        DOM: 'Bfrtip'
    })
})

function detail(stringUrl) {
    $.ajax({
        url: stringUrl
    }).done((res) => {
        $(".modal-title").html(res.name);
        const height = res.height / 10 + "'" + (res.height % 10) + "\"";
        const weight = (res.weight / 10).toFixed(1) + " lbs";
        const gender = "Unknown";
        const category = res.species.name;
        const abilities = res.abilities.map(ability => ability.ability.name).join(', ');

        const types = res.types.map(type => type.type.name).join(', ');

        $("#modalHeight").text(height);
        $("#modalWeight").text(weight);
        $("#modalGender").text(gender);
        $("#modalCategory").text(category);
        $("#modalAbilities").text(abilities);
        $("#modalTypes").html(`<span class="type-background bg-orange">${types.split(',')[0]}</span><span class="type-background bg-blue">${types.split(',')[1]}</span>`);

        $("#hp").css("width", res.stats[0].base_stat + "%").html("HP : " + res.stats[0].base_stat);
        $("#attack").css("width", res.stats[1].base_stat + "%").html("Attack : " + res.stats[1].base_stat);
        $("#defense").css("width", res.stats[2].base_stat + "%").html("Defense : " + res.stats[2].base_stat);
        $("#special-attack").css("width", res.stats[3].base_stat + "%").html("Spesial Attack : " + res.stats[3].base_stat);
        $("#special-defense").css("width", res.stats[4].base_stat + "%").html("Spesial Defense : " + res.stats[4].base_stat);
        $("#speed").css("width", res.stats[5].base_stat + "%").html("Speed : " + res.stats[5].base_stat);
        const moves = res.moves;
        let movesHTML = "";
        for (let i = 0; i < moves.length; i++) {
            movesHTML += `
                <tr>
                    <td>${i + 1}</td>
                    <td>${moves[i].move.name}</td>
                </tr>
            `;
        }
        $("#modalMoves").html(movesHTML);

        fetchPokemonImage(res.id);

        $('#modalPoke').modal('show');
    });
}

function fetchPokemonImage(pokemonId) {
    const $pokemonFirst = $("#pokemonFirst");
    const $pokemonSec = $("#pokemonSec");
    const $pokemonThird = $("#pokemonThird");

    const imageUrl1 = `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/${pokemonId}.png`;
    const imageUrl2 = `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/home/shiny/${pokemonId}.png`;
    const imageUrl3 = `https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/${pokemonId}.png`;

    $pokemonFirst.attr("src", imageUrl1);
    $pokemonSec.attr("src", imageUrl2);
    $pokemonThird.attr("src", imageUrl3);
}