// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const searchWrapper = document.querySelector(".search-input");
const inputBox = searchWrapper.querySelector("input");
const suggBox = searchWrapper.querySelector(".autocom-box");

inputBox.onkeyup = (e) => {
    let userData = e.target.value;
    let emptyArray = [];
    let idArray = [];
    let imgArray = [];
    let finalArray = [];
    if (userData) {
        $.ajax({
            type: "GET",
            url: '/Manga/GetSuggestionList',
            data:
            {
                title: userData,
            },
            //data: '{title: ' + userData + "}",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (resultado) {
                for (let i = 0; i < resultado.resultado.length; i++) {
                    emptyArray.push(resultado.resultado[i].canonicalTitle);
                    idArray.push(resultado.resultado[i].id);
                    imgArray.push(resultado.resultado[i].posterImageLink);
                }
                for (let j = 0; j < emptyArray.length; j++) {
                    if (emptyArray[j].length >= 20) {
                        emptyArray[j] = '<abbr title="' + emptyArray[j] + '">' + emptyArray[j].substr(0,19) + '...</abbr>'
                    }
                    finalArray[j] = '<a href="/Manga/MangaOnPage/' + idArray[j] + '"><li><img src="' + imgArray[j] + ' "><span class="manga-name">' + emptyArray[j] + '</span></li></a>';
                };
                //emptyArray = emptyArray.map((data) => {
                //    return data = '<li>' + data + '</li>';
                //});

                console.log(finalArray);
                console.log(imgArray);
                //console.log(emptyArray);
                searchWrapper.classList.add("active");
                showSuggestions(finalArray);
            },
            error: function () {
                alert("Erro ao buscar mangas");
            }
        });
    }
    else {
        searchWrapper.classList.remove("active");
    }
}


function showSuggestions(list) {
    let listData;
    if (!list.length) {
        userValue = inputBox.value;
        listData = '<li>' + userValue + '</li>';
    } else if (list.length >= 5) {
        let maxLength = 5;
        let newArray = [];
        for (var i = 0; i < maxLength; i++) {
            newArray.push(list[i]);
        }
        listData = newArray.join('');
    }
    else {
        listData = list.join('');
    }
    suggBox.innerHTML = listData;
}