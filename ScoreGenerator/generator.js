let total_player_score = 0;
let total_system_score = 0;

let player_score_counter;
let system_score_counter;

let ratio_counter;

window.onload = function () {
    document.getElementById('name').value = '';

    player_score_counter = document.getElementById('player_score');
    system_score_counter = document.getElementById('system_score');
    ratio_counter = document.getElementById('ratio');

    let player_score_1 = document.getElementsByClassName('player_score_1');
    let player_score_2 = document.getElementsByClassName('player_score_2');
    let player_score_3 = document.getElementsByClassName('player_score_3');

    let system_score_1 = document.getElementsByClassName('system_score_1');
    let system_score_2 = document.getElementsByClassName('system_score_2');
    let system_score_3 = document.getElementsByClassName('system_score_3');

    for (let i = 0; i < player_score_1.length; i++)
        generate_mechanic(player_score_1[i], player_score_1[i].id, 1, true);

    for (let j = 0; j < player_score_2.length; j++)
        generate_mechanic(player_score_2[j], player_score_2[j].id, 2, true);

    for (let k = 0; k < player_score_3.length; k++)
        generate_mechanic(player_score_3[k], player_score_3[k].id, 3, true);

    for (let l = 0; l < system_score_1.length; l++)
        generate_mechanic(system_score_1[l], system_score_1[l].id, 1, false);

    for (let m = 0; m < system_score_2.length; m++)
        generate_mechanic(system_score_2[m], system_score_2[m].id, 2, false);

    for (let n = 0; n < system_score_3.length; n++)
        generate_mechanic(system_score_3[n], system_score_3[n].id, 3, false);
};

function set_player_mechanic(elem, score) {
    total_player_score += (elem.checked ? 1 : -1) * score;
    player_score_counter.innerHTML = total_player_score;

    ratio_counter.innerHTML = get_ratio();
}

function set_system_mechanic(elem, score) {
    total_system_score += (elem.checked ? 1 : -1) * score;
    system_score_counter.innerHTML = total_system_score;

    ratio_counter.innerHTML = get_ratio();
}

function get_ratio() {
    let highest = Math.max(total_player_score, total_system_score);
    if (highest === 0) return 0;

    return round_to_decimal(total_player_score/highest - total_system_score/highest, 2);
}

function round_to_decimal(value, decimal) {
    let factor = Math.pow(10, decimal);
    return Math.round(value * factor) / factor;
}

function generate_mechanic(elem, name, score, is_player) {
    let checkbox = document.createElement('input');
    checkbox.setAttribute('id', name.toLowerCase());
    checkbox.setAttribute('type', 'checkbox');
    checkbox.setAttribute('onclick', is_player
        ? 'set_player_mechanic(this, ' + score + ')'
        : 'set_system_mechanic(this, ' + score + ')');
    elem.appendChild(checkbox);

    let label = document.createElement('label');
    label.setAttribute('for', checkbox.id);
    label.innerHTML = name;
    elem.appendChild(label);
}