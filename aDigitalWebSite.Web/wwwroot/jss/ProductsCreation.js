function CreateProduct(){
    var title = $("#title").val();
    var description = $("#description").val();
    var startsAt = $("#startsAt").val();
    var minimum = $("#minimum").val();
    if(title == "" || startsAt == ""){ 
        alert("\"Nome\" e \"A partir de\" são obrigatórios");
        return;
    }
    disableCreateButton();
    var tags = GetSelectedTagsString();
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
    url:productServiceRootUrl,
    success:function (data){
        alert('Item Cadastrado Com Sucesso');
        $("#title").val("");
        $("#description").val("");
        $("#startsAt").val("");
        $("#minimum").val("");
        ClearTagSelection();
        enableCreateButton();
    },
    error:function (data){
        alert('Ocorreu um erro ao cadastrar este item. Tente novamente mais tarde.');
        enableCreateButton();
    }
    });
}

function disableCreateButton(){
    var btn = $("#buttonCreate");
    btn.attr('disabled','disabled');
    btn.val('Cadastrando...');
}

function enableCreateButton(){
    var btn = $("#buttonCreate");
    btn.removeAttr('disabled');
    btn.val('Cadastrar');
}

function GetTags(){
    $.ajax({
        method:"GET",
        url:tagServiceRootUrl,
        success:function (data){
            var $model = $("#tagModel");
            data.forEach(function(ele){
                var $newItem = $model.clone();
                $newItem.html(ele);
                $newItem.on('click',function(e){
                    if(this.classList.contains('tag-selected')){
                        UnselectTag(this);
                    }
                    else{
                        SelectTag(this)
                    }
                });
                $model.parent().append($newItem);
                $newItem.show();
            });
        }
        });
}

function GetSelectedTagsString(){
    var auxEle = $(".tag-selected");
    if(!auxEle){
        return "";
    }
    var result = "";
    auxEle.toArray().forEach(function (item){
        result += (" "+item.innerText);
    });
    return result;
}

function ClearTagSelection(){
    var auxEle = $(".tag-selected");
    if(!auxEle){
        return "";
    }
    var result = "";
    auxEle.toArray().forEach(function (item){
        UnselectTag(item);
    });
}

function UnselectTag(tagElement){
    tagElement.classList.add('label-info');
    tagElement.classList.remove('tag-selected');
}

function SelectTag(tagElement){
    tagElement.classList.remove('label-info');
    tagElement.classList.add('tag-selected');
}