

var handleSearchPost = () => {
    var searchResultsDiv = document.getElementById("searchResults");
    var searchTerm = document.getElementById("searchTerm").value;
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Home/Search?searchTerm=" + searchTerm, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
            searchResultsDiv.innerHTML = xhr.responseText;
        }
    };
    xhr.send();
}

var handlePagingPost = (page) => {
    var pagingResultsDiv = document.getElementById("pagingResults");
    console.log(pagingResultsDiv);
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Home/Paging?page=" + page, true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
            pagingResultsDiv.innerHTML = xhr.responseText;
        }
    };
    xhr.send();
}

window.onload = () => {
    handlePagingPost();
};