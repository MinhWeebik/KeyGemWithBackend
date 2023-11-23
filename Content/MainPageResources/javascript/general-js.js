let option1Parent = document.querySelector('.option-1');
let option1Child = option1Parent.querySelector('.option-line-animation');
let option2Parent = document.querySelector('.option-2');
let option2Child = option2Parent.querySelector('.option-line-animation');
let grid1 = document.querySelector('.featured-collection-grid');
let grid2 = document.querySelector('.featured-collection-grid:nth-child(2)');


function newFocus()
{
  option2Child.classList.remove('option-active');
  option2Child.classList.add('option-not-active');
  option1Child.classList.remove('option-not-active');
  option1Child.classList.add('option-active');
  grid1.classList.remove('grid-hidden');
  grid1.classList.remove('grid-dissappear');
  grid2.classList.add('grid-hidden');
  grid2.classList.add('grid-dissappear');
}

function bestSellerFocus()
{
  option2Child.classList.remove('option-not-active');
  option2Child.classList.add('option-active');
  option1Child.classList.remove('option-active');
  option1Child.classList.add('option-not-active');
  grid2.classList.remove('grid-hidden');
  grid2.classList.remove('grid-dissappear');
  grid1.classList.add('grid-hidden');
  grid1.classList.add('grid-dissappear');
}

const observer = new IntersectionObserver((entries) => {
  entries.forEach((entry)=> {
    if (entry.isIntersecting)
    {
      entry.target.classList.add('show-image');
    }
  })
})

const hiddenElement =   document.querySelectorAll('.catagory-image');
const hiddenElement2 = document.querySelector('.newsletter-logo');
hiddenElement.forEach((el) => {
  observer.observe(el);
})
observer.observe(hiddenElement2);