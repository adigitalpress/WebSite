function CreateProduct(){
    var title = $("#title").val();
    var description = $("#description").val();
    var startsAt = $("#startsAt").val();
    var tags = $("#tags").val();
    var minimum = $("#minimum").val();
    if(title == "" || startsAt == ""){ 
        alert("\"Nome\" e \"A partir de\" são obrigatórios")
    }
    var pack = {
        title:title, 
        description:description, 
        startsAt:startsAt, 
        tags:tags,
        minimal:minimum
        };
    $.ajax({
    data: JSON.stringify(pack),
    contentType: "application/json",
    method:"PUT",
    url:"http://localhost:5003",
    success:function (data){
        alert('Item Cadastrado Com Sucesso');
        $("#title").val("");
        $("#description").val("");
        $("#startsAt").val("");
        $("#tags").val("");
        $("#minimum").val("");
    }
    })
}

function GetTags(){
    $.ajax({
        method:"GET",
        url:"http://localhost:5002",
        success:function (data){
            var $model = $("#tagModel");
            data.forEach(function(ele){
                var $newItem = $model.clone();
                $newItem.html(ele);
                $newItem.on('click',function(e){
                    if(this.classList.contains('tag-selected')){
                        this.classList.add('label-info');
                        this.classList.remove('tag-selected');
                    }
                    else{
                        this.classList.remove('label-info');
                        this.classList.add('tag-selected');
                    }
                });
                $model.parent().append($newItem);
                $newItem.show();
            });
        }
        });
}