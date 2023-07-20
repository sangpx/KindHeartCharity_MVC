var handleDelete = () => {
  var modalDelete = document.getElementById("deleteModal");
  var btnDeletes = document.querySelectorAll(".btn-delete");
  var btnDeleteConfirm = document.querySelector(".btn-delete-confirm");
  var btnCanel = document.querySelector(".btn-cancel");
  var overlay = document.querySelector(".overlay");

  btnDeletes.forEach((item) => {
    item.onclick = () => {
      // <a type="button" class="btn-cancel close" data-dismiss="modal"><i class="bi bi-x-lg"></i></a>
      modalDelete.classList.add("show");
      overlay.classList.add("show");
      var postId = item.getAttribute("data-post-id");
      btnDeleteConfirm.addEventListener("click", () => {
        try {
          var xhr = new XMLHttpRequest();
          xhr.open("DELETE", `/Admin/DeleteConfirmed/${postId}`, true);
          xhr.setRequestHeader("Content-Type", "application/json");
          xhr.onreadystatechange = () => {
            if (xhr.readyState === XMLHttpRequest.DONE) {
              if (xhr.readyState === 4 && xhr.status === 200) {
                console.log("success");
                loadPosts();
              } else {
                console.log("Error deleting data.");
              }
            }
          };
          xhr.send();
        } catch (error) {
          console.log(error);
        }
      });

      btnCanel.onclick = () => {
        modalDelete.classList.remove("show");
        overlay.classList.remove("show");
      };
    };
  });
};

var loadPosts = () => {
  try {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Admin/GetAllPost", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.onreadystatechange = () => {
      if (xhr.readyState === 4 && xhr.status === 200) {
        var posts = JSON.parse(xhr.responseText);
        var postListHtml = `<table class="table table-striped">
                    <thead class="thead-dark">
                        <tr>
                            <th style="width: 50px">
                               STT
                            </th>
                            <th style="width: 350px">
                               Content
                            </th>
                            <th style="width: 300px">
                               Description
                            </th>
                            <th style="width: 200px">
                                PostImageURL
                            </th>
                            <th style="width: 200px">
                               PostDate
                            </th>
                            <th style="width: 200px">
                               Action
                            </th>
                            
                         </tr>
                    </thead>
                    <tbody>
                `;

        posts.forEach((post, index) => {
          postListHtml += `
                        <tr>
                            <td>
                               ${index + 1} 
                            </td>
                            <td style="text-align:left">
                               ${post.content} 
                            </td>
                            <td style="text-align:left">
                                ${post.description} 
                            </td>
                            <td>
                                <img style="height:60px;width:80px;" src="/Uploads/${
                                  post.postImageURL
                                }" />
                            </td>
                            <td>
                                ${post.postDate} 
                            </td>
                            <td>
                                <button class="btn btn-update text-lg-right"><i class="bi bi-pencil-square"></i></button> |
                                <button  data-post-id=${
                                  post.postId
                                }   data-bs-toggle="modal" data-bs-target="#deleteModal" class="btn btn-delete text-lg-right"><i class="bi bi-trash"></i></button>
                            </td>
                        </tr>

                      <div class="overlay"></div>
                          <div class="modal" id="deleteModal">
                              <div class="modal-dialog">
                                <div class="modal-content">
                                  <div class="modal-header">
                                    <h4 class="modal-title">Are you sure?</h4>
                                
                                  </div>
                                  <div class="modal-body">
                                    <p>Do you really want to delete these records? This process cannot be undone.</p>
                                  </div>
                                  <div class="modal-footer">
                                    <button type="button" class="btn btn-default btn-cancel" data-dismiss="modal">Close</button>
                                    <button class="btn btn-danger btn-delete-confirm">Delete</button>
                                  </div>
                                </div>[]
                              </div>
                          </div>
                        
                    `;
        });
        postListHtml += "</tbody>" + "</table>";
        var postList = document.getElementById("result");
        postList.innerHTML = postListHtml;
        handleDelete();
      } else if (xhr.readyState === 4) {
        alert("Failed to get post list.");
      }
    };
    xhr.send();
  } catch (error) {
    console.log(error);
  }
};

var updatePost = () => {
  console.log("update");
};

window.onload = () => {
  loadPosts();
};
