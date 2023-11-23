let grid = document.querySelector('.item-grid');
let children = [...grid.children];
let sortItem1 = document.querySelector('.sort-item');
let sortItem2 = document.querySelector('.sort-item:nth-child(2)');
let sortItem3 = document.querySelector('.sort-item:nth-child(3)');
let sortItem4 = document.querySelector('.sort-item:nth-child(4)');

function resetCurrentSort()
{
  sortItem1.classList.remove('current');
  sortItem2.classList.remove('current');
  sortItem3.classList.remove('current');
  sortItem4.classList.remove('current');
}

function sortLowToHighPrice()
{
  children.sort((a,b) =>{
    return parseFloat(a.querySelector('.item-price').innerText.replace('$ ','')) > parseFloat(b.querySelector('.item-price').innerText.replace('$ ','')) ? 1 : -1;
  });
  grid.innerHTML = '';
  grid.classList.add('fade-grid-in');
  setTimeout(function() {
    grid.classList.remove('fade-grid-in');
  },1000);
  children.forEach((item) => grid.appendChild(item));
  resetCurrentSort();
  sortItem3.classList.add('current');
}

function sortHighToLowPrice()
{
  children.sort((a,b) =>{
    return parseFloat(a.querySelector('.item-price').innerText.replace('$ ','')) < parseFloat(b.querySelector('.item-price').innerText.replace('$ ','')) ? 1 : -1;
  });
  grid.innerHTML = '';
  grid.classList.add('fade-grid-in');
  setTimeout(function() {
    grid.classList.remove('fade-grid-in');
  },1000);
  children.forEach((item) => grid.appendChild(item));
  resetCurrentSort();
  sortItem4.classList.add('current');
}

function sortFromAToZ()
{
  children.sort((a,b) =>{
    return a.querySelector('.item-info').innerText.replace('"','') > b.querySelector('.item-info').innerText.replace('"','') ? 1 : -1;
  });
  grid.innerHTML = '';
  grid.classList.add('fade-grid-in');
  setTimeout(function() {
    grid.classList.remove('fade-grid-in');
  },1000);
  children.forEach((item) => grid.appendChild(item));
  resetCurrentSort();
  sortItem1.classList.add('current');
}

function sortFromZToA()
{
  children.sort((a,b) =>{
    return a.querySelector('.item-info').innerText.replace('"','') < b.querySelector('.item-info').innerText.replace('"','') ? 1 : -1;
  });
  grid.innerHTML = '';
  grid.classList.add('fade-grid-in');
  setTimeout(function() {
    grid.classList.remove('fade-grid-in');
  },1000);
  children.forEach((item) => grid.appendChild(item));
  resetCurrentSort();
  sortItem2.classList.add('current');
}

