let livros = [];
let autores = [];
let idSelecionado;
const ul = document.querySelector("ul");
const formLivro = document.getElementById("formLivro");
const formLogin = document.getElementById("formLogin");
const buttonExcluir = document.getElementById("buttonExcluir");
const buttonCancelar = document.getElementById("buttonCancelar");
const buttonSalvar = document.getElementById("buttonSalvar");

async function init() {
  formLogin.addEventListener("submit", fazLogin);
  buttonCancelar.addEventListener("click", limpaSelecao);
  formLivro.addEventListener("submit", salvaLivro);
  buttonExcluir.addEventListener("click", deletarLivro);

mostraTelaLivros();

  /*if (isLogado()) {
    mostraTelaLivros();
  } else {
    mostraTelaLogin();
  }*/
}
init();

function mostraTelaLogin() {
  document.getElementById("telaLivros").style.display = "none";
  document.getElementById("telaLogin").style.display = "block";
}

async function mostraTelaLivros() {
  document.getElementById("telaLivros").style.display = "block";
  document.getElementById("telaLogin").style.display = "none";
  [livros, autores] = await Promise.all([listaLivros(), listaAutores()]);
  exibeOpcoesAutores();
  exibeLivros();
  limpaSelecao();
}

async function fazLogin(evt) {
  evt.preventDefault();
  try {
    await login(formLogin.email.value, formLogin.senha.value);
    mostraTelaLivros();
  } catch (erro) {
    mostraErro("Falha no login. Verifique e-mail e senha.");
  }
}

function selecionaItem(livro, li) {
  limpaSelecao();
  idSelecionado = livro.codigo;
  li.classList.add("selected");

  formLivro.descricao.value = livro.descricao;
  formLivro.isbn.value = livro.isbn;
  formLivro.autorId.value = livro.autorId;
  formLivro.anoLancamento.valueAsNumber = livro.anoLancamento;

  buttonExcluir.style.display = "inline";
  buttonCancelar.style.display = "inline";
  buttonSalvar.textContent = "Atualizar";
}

function limpaSelecao() {
  limpaErros();
  idSelecionado = undefined;
  const li = ul.querySelector(".selected");

  if (li) {
    li.classList.remove("selected");
  }

  formLivro.descricao.value = "";
  formLivro.isbn.value = "";
  formLivro.autorId.value = "";
  formLivro.anoLancamento.value = "";

  buttonExcluir.style.display = "none";
  buttonCancelar.style.display = "none";
  buttonSalvar.textContent = "Cadastrar";
}

async function salvaLivro(evt) {

  evt.preventDefault();
  const livroPayload = {
    id: idSelecionado,
    descricao: formLivro.descricao.value,
    isbn: formLivro.isbn.value,
    autorId: formLivro.autorId.value,
    anoLancamento: formLivro.anoLancamento.valueAsNumber,
  };
  if (
    !livroPayload.descricao ||
    !livroPayload.isbn ||
    !livroPayload.autorId ||
    !livroPayload.anoLancamento
  ) {
    mostraErro("Prencha todos os campos.");
  } else {
    if (idSelecionado) {
      await atualizaLivro(livroPayload);
    } else {
      await criaLivro(livroPayload);
    }
    livros = await listaLivros();
    window.location.reload();
    exibeLivros();
  }
}

async function deletarLivro() {

  if (idSelecionado) {
    await excluiLivro(idSelecionado);
    livros = await listaLivros();
    limpaSelecao();
    window.location.reload();
    exibeLivros();
  }
}

function exibeLivros() {
  ul.innerHTML = "";
  for (const livro of livros) {
    const li = document.createElement("li");
    const divDescricao = document.createElement("div");
    divDescricao.textContent = livro.descricao;
    li.appendChild(divDescricao);
    const divAutor = document.createElement("div");
    divAutor.textContent = livro.nomeAutor;
    li.appendChild(divAutor);
    const divAno = document.createElement("div");
    divAno.textContent = livro.anoLancamento;
    li.appendChild(divAno);
    ul.appendChild(li);

    if (idSelecionado === livro.codigo) {
      li.classList.add("selected");
    }
    li.addEventListener("click", () => selecionaItem(livro, li));
  }
  
}

function exibeOpcoesAutores() {
  formLivro.autorId.innerHTML = "";
  for (const autor of autores) {
    const option = document.createElement("option");
    option.textContent = autor.nome;
    //option.value = autor.id;
    option.value = autor.autorId;
    formLivro.autorId.appendChild(option);
  }
}

function mostraErro(message, error) {
  document.getElementById("errors").textContent = message;
  if (error) {
    console.error(error);
  }
}

function limpaErros() {
  document.getElementById("errors").textContent = "";
}
