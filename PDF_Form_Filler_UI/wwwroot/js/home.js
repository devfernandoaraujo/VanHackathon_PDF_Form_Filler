window.onload = function () {
    //load the information in the grid
    this.loadGridCandidates();  

    /*Wait message while the server is working */
    $(document).ajaxStart(function () {
        $("#loading-div-background").show();
    });
    $(document).ajaxComplete(function () {
        $("#loading-div-background").hide();
    });
}

/* Request list of candidates for the Global Talent Stream Application */
async function loadGridCandidates() {

    let exFunction = this;

    $.ajax({
        url: "/Home/getAllCandidates",
        type: "GET",
        success: async function(data) {
            await data.forEach(async function(element) {
                await exFunction.generateGridRow(element);
            });
        },
        error: function(error) {
            console.log(`Error on load grid of candidates. Check error : ${error}`);
        }
    })
}

/* Generate the rowns in the grid */
async function generateGridRow(element) {
    let grid = document.getElementById("gridCandidates");

    let row = document.createElement("div");

    row.classList.add("row");
    row.classList.add("bg-light");

    let column = await generateGridColumn(element.firstName);
    row.appendChild(column);
    column = await generateGridColumn(element.middleName);
    row.appendChild(column);
    column = await generateGridColumn(element.lastName);
    row.appendChild(column);
    column = await generateGridColumn(element.country);
    row.appendChild(column);

    column = await generateGridColumn("");
    column.setAttribute("candidate_id", element.id);

    //Add form image
    let img = document.createElement("img");

    /*Properties*/
    img.setAttribute("src", "../images/application-form.jpg");
    img.classList.add("rounded");
    img.classList.add("mx-auto");
    img.classList.add("d-block");
    img.classList.add("cursor-pointer");
    img.setAttribute("title", "Generate form application");

    //Events 
    img.addEventListener("click",
        function () {

            const candidate_id = $(this).parent().attr("candidate_id");
            generateForm(candidate_id);
        },
        false);

    column.appendChild(img);

    row.appendChild(column);

    grid.appendChild(row);

} 

//Generate all columns for the grid
function generateGridColumn(value) {
    //Generation the columns 
    let column = document.createElement("div");
    column.classList.add("col-sm");
    column.classList.add("border");

    column.innerHTML = value;

    return column;
}

//Download the pdf fro the server
function generateForm(candidate_id) {

    window.location.href = `/Home/generateFormApplication?candidateId=${candidate_id}`;

}

